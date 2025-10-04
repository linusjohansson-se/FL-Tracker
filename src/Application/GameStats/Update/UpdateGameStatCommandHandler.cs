using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.GameStats;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.GameStats.Update;

internal sealed class UpdateGameStatCommandHandler(
	IApplicationDbContext context,
	IDateTimeProvider dateTimeProvider)
	: ICommandHandler<UpdateGameStatCommand>
{
	public async Task<Result> Handle(UpdateGameStatCommand command, CancellationToken cancellationToken)
	{
		GameStat? gameStat = await context.GameStats
			.FirstOrDefaultAsync(gs => gs.Id == command.GameStatId, cancellationToken);

		if (gameStat is null)
		{
			return Result.Failure(GameStatErrors.NotFound());
		}

		gameStat.Update(
			command.Kills,
			command.Assists,
			command.Deaths,
			command.Adr,
			command.HsRatio,
			command.ClutchRatio,
			command.Fkpr,
			command.Fdpr,
			command.RoundsPlayed,
			command.TotalDamage,
			command.HeadshotKills,
			dateTimeProvider.UtcNow
		);

		await context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}