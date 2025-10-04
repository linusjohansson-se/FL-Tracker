using Application.GameStats.Create;
using Application.GameStats.GetById;
using Application.Players.Create;
using Application.Players.GetBySteamId;
using DemoFile;
using DemoFile.Game.Cs;
using Domain.Players;
using Domain.Services;
using MediatR;
using SharedKernel;

namespace Application.Services;

public class GameService : IGameService
{
	private readonly IDateTimeProvider _dateTimeProvider;

	private readonly ISender _sender;

	private GameService(IDateTimeProvider dateTimeProvider, ISender sender)
	{
		_dateTimeProvider = dateTimeProvider;
		_sender = sender;
	}

	public async Task<Result> ProcessGameAsync(FileStream file)
	{
		var demo = new CsDemoParser();

		var roundParticipants = new HashSet<ulong>();
		var stats = new Dictionary<ulong, GameStatResponse>();
		var names  = new Dictionary<ulong, string?>();

		demo.Source1GameEvents.PlayerConnect += e =>
		{
			if (e.SteamId is { } sid)
				names[sid] = e.Name;
		};
		demo.Source1GameEvents.PlayerInfo += e =>
		{
			if (e.Player?.SteamID is { } sid)
				names[sid] = e.Player.PlayerName;
		};
		
		
		demo.Source1GameEvents.PlayerDeath += e =>
		{
			var victimSid   = e.Player?.SteamID ?? 0UL;
			var attackerSid = e.Attacker?.SteamID ?? 0UL;

			if (attackerSid != 0 && attackerSid != victimSid)
			{
				ref var attacker = ref GetOrInit(stats, attackerSid, names);
				attacker.Kills++;
			}

			if (victimSid != 0)
			{
				ref var victim = ref GetOrInit(stats, victimSid, names);
				victim.Deaths++;
			}
			
			var assisterSid = e.Assister?.SteamID ?? 0UL;
			if (assisterSid != 0 && assisterSid != attackerSid && assisterSid != victimSid)
			{
				ref var assister = ref GetOrInit(stats, assisterSid, names);
				assister.Assists++;
			}

			if (e.Headshot)
			{
				ref var attacker = ref GetOrInit(stats, attackerSid, names);
				attacker.HeadshotKills++;
			}
			
		};
			
		demo.Source1GameEvents.PlayerHurt += e =>
		{
			var victimSid   = e.Player?.SteamID ?? 0UL;
			var attackerSid = e.Attacker?.SteamID ?? 0UL;

			// mark participation
			if (victimSid != 0)  roundParticipants.Add(victimSid);
			if (attackerSid != 0) roundParticipants.Add(attackerSid);

			// ignore self damage and team damage (if team info is available)
			if (attackerSid == 0 || attackerSid == victimSid) return;

			if (victimSid != 0 && e.Attacker.Team == e.Player.Team) return;
			
			var dmg = e.DmgHealth;
			if (dmg <= 0) return;

			ref var attacker = ref GetOrInit(stats, attackerSid, names);
			attacker.TotalDamage += dmg;
		};
		
		demo.Source1GameEvents.RoundStart += _ =>
		{
			roundParticipants.Clear();
		};
		
		demo.Source1GameEvents.RoundEnd += _ =>
		{
			foreach (var sid in roundParticipants)
			{
				ref var s = ref GetOrInit(stats, sid, names);
				s.RoundsPlayed++;
			}
			roundParticipants.Clear();
		};
		
		var reader = DemoFileReader.Create(demo, file);
		await reader.ReadAllAsync();

		// Persist players and their stats
		foreach (var (steamId, s) in stats)
		{
			var playerId = await GetOrCreatePlayerAsync(steamId.ToString(), names.GetValueOrDefault(steamId));
			// Save stats (adapt to your domain types)
			s.PlayerId = playerId.Value;
			var save = await _sender.Send(new CreateGameStatCommand
			{
				PlayerId = s.PlayerId,
				Adr = s.TotalDamage / s.RoundsPlayed,
				Assists = s.Assists,
				ClutchRatio = s.ClutchRatio,
				Deaths = s.Deaths,
				Fdpr = s.Fdpr,
				Fkpr = s.Fkpr,
				HsRatio = s.HeadshotKills / s.Kills,
				Kills = s.Kills,
				RoundsPlayed = s.RoundsPlayed,
				TotalDamage = s.TotalDamage,
				HeadshotKills = s.HeadshotKills
			});
			if (save.IsFailure) return new Result(false, save.Error);
		}
		
		return new Result(true, Error.None);
	}

	private async Task<Result<Guid>> GetOrCreatePlayerAsync(string steamId, string playerName)
	{
		Result<PlayerResponse> stored_player =
			await _sender.Send(new GetPlayerBySteamIdQuery(steamId));

		if (stored_player.IsFailure)
		{
			var created_player = await _sender.Send(new CreatePlayerCommand
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
		else
		{
			return stored_player.Value.Id;
		}
	}
	
	static ref GameStatResponse GetOrInit(
		Dictionary<ulong, GameStatResponse> dict, ulong sid, Dictionary<ulong, string?> names)
	{
		if (!dict.TryGetValue(sid, out var s))
		{
			s = new GameStatResponse
			{
				Id = Guid.NewGuid(),
				Kills = 0,
				Deaths = 0,
				Assists = 0,
				RoundsPlayed = 0,
				TotalDamage = 0,
				HeadshotKills = 0
			};
			dict[sid] = s;
		}
		return ref dict[sid];
	}
}