using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Games;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Games.Delete;

internal sealed class DeleteGameCommandHandler(IApplicationDbContext context)
	: ICommandHandler<DeleteGameCommand>
{
	public async Task<Result> Handle(DeleteGameCommand command, CancellationToken cancellationToken)
	{
		Game? game = await context.Games
			.FirstOrDefaultAsync(g => g.Id == command.GameId, cancellationToken);

		if (game is null)
		{
			return Result.Failure(GameErrors.NotFound());
		}

		context.Games.Remove(game);

		await context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}