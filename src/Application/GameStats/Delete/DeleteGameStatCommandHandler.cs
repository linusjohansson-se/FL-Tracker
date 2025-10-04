using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Games;
using Domain.GameStats;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.GameStats.Delete;

internal sealed class DeleteGameStatCommandHandler(IApplicationDbContext context)
	: ICommandHandler<DeleteGameStatCommand>
{
	public async Task<Result> Handle(DeleteGameStatCommand command, CancellationToken cancellationToken)
	{
		GameStat? gameStat = await context.GameStats
			.FirstOrDefaultAsync(gs => gs.Id == command.GameStatId, cancellationToken);

		if (gameStat is null)
		{
			return Result.Failure(GameErrors.NotFound());
		}

		context.GameStats.Remove(gameStat);

		await context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}