using Application.Games.Create;
using Application.GameStats.Create;
using Application.GameStats.GetById;
using Application.Players.Create;
using Application.Players.GetBySteamId;
using DemoFile;
using DemoFile.Game.Cs;
using Domain.Services;
using MediatR;
using SharedKernel;

namespace Application.Services;

public class GameService : IGameService
{
	private readonly IDateTimeProvider _dateTimeProvider;

	private readonly ISender _sender;

	public GameService(IDateTimeProvider dateTimeProvider, ISender sender)
	{
		_dateTimeProvider = dateTimeProvider;
		_sender = sender;
	}
	
	

	public async Task<Result> ProcessGameAsync(FileStream file)
	{
		CsDemoParser demo = new CsDemoParser();

		HashSet<ulong> roundParticipants = new HashSet<ulong>();
		Dictionary<ulong, GameStatResponse> stats = new Dictionary<ulong, GameStatResponse>();
		Dictionary<ulong, string?> names = new Dictionary<ulong, string?>();
		HashSet<CCSTeam> teams = new HashSet<CCSTeam>();
		var gameDate = _dateTimeProvider.UtcNow;
		string map = String.Empty;
		string matchId = string.Empty;
		
		void CaptureName(CCSPlayerController? pc)
		{
			if (pc == null) return;
			
			ulong sid = pc.SteamID;
			if (sid == 0) return;

			var nick = pc.PlayerName;
			if (!string.IsNullOrWhiteSpace(nick))
				names[sid] = nick;
			else if (!names.ContainsKey(sid))
				names[sid] = nick; // may be empty early; OK as placeholder
		}
		
		void ResetForNewMatch()
		{
			Console.WriteLine("== New match detected: clearing aggregates ==");
			stats.Clear();
			teams.Clear();
			Console.WriteLine("Time " +demo.CurrentGameTime.Value);
			matchId = demo.FileHeader.DemoFileStamp;
			map = demo.ServerInfo.MapName;
			Console.WriteLine("Match Id: " + matchId);
		}

		demo.Source1GameEvents.BeginNewMatch += _ => ResetForNewMatch();
		
		demo.Source1GameEvents.PlayerConnect += e =>
		{
			CaptureName(e.Player);
		};
		demo.Source1GameEvents.PlayerInfo += e =>
		{
			CaptureName(e.Player);
		};


		demo.Source1GameEvents.PlayerDeath += e =>
		{
			ulong victimSid = e.Player?.SteamID ?? 0UL;
			ulong attackerSid = e.Attacker?.SteamID ?? 0UL;
			
			CaptureName(e.Player);
			CaptureName(e.Attacker);
			CaptureName(e.Assister);

			if (attackerSid != 0 && attackerSid != victimSid)
			{
				GameStatResponse attacker = GetOrInit(stats, attackerSid, names);
				attacker.Kills++;
			}

			if (victimSid != 0)
			{
				GameStatResponse victim = GetOrInit(stats, victimSid, names);
				victim.Deaths++;
			}

			ulong assisterSid = e.Assister?.SteamID ?? 0UL;
			if (assisterSid != 0 && assisterSid != attackerSid && assisterSid != victimSid)
			{
				GameStatResponse assister = GetOrInit(stats, assisterSid, names);
				assister.Assists++;
			}

			if (e.Headshot)
			{
				GameStatResponse attacker = GetOrInit(stats, attackerSid, names);
				attacker.HeadshotKills++;
			}
		};


		demo.Source1GameEvents.PlayerHurt += e =>
		{
			ulong victimSid = e.Player?.SteamID ?? 0UL;
			ulong attackerSid = e.Attacker?.SteamID ?? 0UL;
			
			CaptureName(e.Player);
			CaptureName(e.Attacker);
			

			if (attackerSid == 0 || attackerSid == victimSid)
			{
				return;
			}

			if (victimSid != 0 && e.Attacker.Team == e.Player.Team)
			{
				return;
			}

			int dmg = e.DmgHealth;
			if (dmg <= 0)
			{
				return;
			}

			GameStatResponse attacker = GetOrInit(stats, attackerSid, names);
			attacker.TotalDamage += dmg;
		};

		demo.Source1GameEvents.RoundStart += _ =>
		{
			if (!teams.Where(t => t.Score > 0).Any() && demo.GameRules.TotalRoundsPlayed == 0)
			{
				ResetForNewMatch();
			}
			roundParticipants.Clear();
			
			Console.WriteLine($"----------------------------------");
			Console.WriteLine($"Round {demo.GameRules.TotalRoundsPlayed + 1} starting");
			Console.WriteLine($"----------------------------------");
			
			foreach (var inDemoPlayer in demo.Players)
			{ 
				CaptureName(inDemoPlayer);
				roundParticipants.Add(inDemoPlayer.SteamID);
				if (inDemoPlayer.Team.Teamname != "Unassigned")
				{
					teams.Add(inDemoPlayer.Team);	
				}
			}
		};

		demo.Source1GameEvents.RoundEnd += _ =>
		{
			foreach (ulong sid in roundParticipants)
			{
				GameStatResponse s = GetOrInit(stats, sid, names);
				s.RoundsPlayed++;
			}
			
			roundParticipants.Clear();
			
			Console.WriteLine($"----------------------------------");
			Console.WriteLine("Round ended");
			Console.WriteLine($"----------------------------------");
			Console.WriteLine($"Current score:");
			foreach (var team in teams)
			{
				Console.WriteLine($"Team: {team.ClanTeamname} - {team.Teamname}");
				Console.WriteLine($"Score: {team.Score}");
			}
			Console.WriteLine($"----------------------------------");
			
		};

		DemoFileReader<CsDemoParser> reader = DemoFileReader.Create(demo, file);
		await reader.ReadAllAsync();
		
		var teamList = teams.ToList();
		var game = await _sender.Send(new CreateGameCommand
		{
			Date = gameDate,
			TeamA = null,
			TeamB = null,
			TeamAName = teamList[0].Teamname,
			TeamBName = teamList[1].Teamname,
			TeamAScore = teamList[0].Score,
			TeamBScore = teamList[1].Score,
			Winner = null,
			MatchId = matchId
		});

		if (game.IsFailure)
		{
			return new Result(false, game.Error);
		}

		foreach ((ulong steamId, GameStatResponse s) in stats)
		{
			Result<Guid> playerId = await GetOrCreatePlayerAsync(steamId.ToString(), names.GetValueOrDefault(steamId));
			s.PlayerId = playerId.Value;

			double hsRatio = (double)s.HeadshotKills / (double)s.Kills;
			
			Result<Guid> save = await _sender.Send(new CreateGameStatCommand
			{
				PlayerId = s.PlayerId,
				Adr = (double)s.TotalDamage / (double)s.RoundsPlayed,
				Assists = s.Assists,
				ClutchRatio = (double)s.ClutchAttempts > 0 ? (double)s.ClutchWins / (double)s.ClutchAttempts : 0.0,
				Deaths = s.Deaths,
				Fdpr = (double)s.FirstDeaths / (double)s.RoundsPlayed,
				Fkpr = (double)s.FirstKills / (double)s.RoundsPlayed,
				HsRatio = hsRatio,
				Kills = s.Kills,
				RoundsPlayed = s.RoundsPlayed,
				TotalDamage = s.TotalDamage,
				HeadshotKills = s.HeadshotKills,
				ClutchAttempts = s.ClutchAttempts,
				ClutchWins = s.ClutchWins,
				FirstKills = s.FirstKills,
				FirstDeaths = s.FirstDeaths,
				GameId = game.Value
			});
			if (save.IsFailure)
			{
				return new Result(false, save.Error);
			}
		}

		return new Result(true, Error.None);
	}

	private async Task<Result<Guid>> GetOrCreatePlayerAsync(string steamId, string playerName)
	{
		Result<PlayerResponse> stored_player =
			await _sender.Send(new GetPlayerBySteamIdQuery(steamId));

		if (stored_player.IsFailure)
		{
			Result<Guid> created_player = await _sender.Send(new CreatePlayerCommand
			{
				TeamId = null,
				SteamId = steamId,
				Nickname = playerName,
				LeetifyId = null,
				FaceitId = null
			});

			if (created_player.IsFailure)
			{
				return new Result<Guid>(Guid.Empty, false, stored_player.Error);
			}

			return created_player.Value;
		}

		return stored_player.Value.Id;
	}

	private static GameStatResponse GetOrInit(
		Dictionary<ulong, GameStatResponse> dict, ulong sid, Dictionary<ulong, string?> names)
	{
		if (!dict.TryGetValue(sid, out GameStatResponse? s))
		{
			s = new GameStatResponse
			{
				Id = Guid.NewGuid(),
				Kills = 0,
				Deaths = 0,
				Assists = 0,
				RoundsPlayed = 0,
				TotalDamage = 0,
				HeadshotKills = 0,
				ClutchAttempts = 0,
				ClutchWins = 0,
				FirstDeaths = 0,
				FirstKills = 0
			};
			dict[sid] = s;
		}

		return s;
	}
	
}