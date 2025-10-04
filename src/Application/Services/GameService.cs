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
		CsDemoParser demo = new CsDemoParser();

		List<Player> players = new List<Player>();

		foreach (CCSPlayerController player in demo.Players)
		{
			var playerId = Guid.NewGuid();

			Result<PlayerResponse> stored_player =
				await _sender.Send(new GetPlayerBySteamIdQuery(player.SteamID.ToString()));

			if (stored_player.IsFailure)
			{
				var created_player = await _sender.Send(new CreatePlayerCommand
				{
					TeamId = null,
					SteamId = player.SteamID.ToString(),
					Nickname = player.PlayerName,
					LeetifyId = null,
					FaceitId = null
				});

				if (created_player.IsFailure)
				{
					return new Result(false, stored_player.Error);
				}

				playerId = created_player.Value;
			}
			else
			{
				playerId = stored_player.Value.Id;
			}

			GameStatResponse trackedPlayer = new()
			{
				Id = Guid.NewGuid(), PlayerId = playerId, Kills = player.KillCount, Assists = player.
			}

		}

		return new Result(true, Error.None);
	}
}