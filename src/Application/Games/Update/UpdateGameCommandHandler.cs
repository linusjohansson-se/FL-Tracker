using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Games;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Games.Update;

internal sealed class UpdateGameCommandHandler(
	IApplicationDbContext context,
	IDateTimeProvider dateTimeProvider)
	: ICommandHandler<UpdateGameCommand>
{
	public async Task<Result> Handle(UpdateGameCommand command, CancellationToken cancellationToken)
	{
		Game? game = await context.Games
			.FirstOrDefaultAsync(g => g.Id == command.GameId, cancellationToken);

		if (game is null)
		{
			return Result.Failure(GameErrors.NotFound());
		}

		game.Update(
			command.Date,
			command.TeamAScore,
			command.TeamBScore,
			command.Winner,
			dateTimeProvider.UtcNow
		);

		await context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}