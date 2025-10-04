using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Players;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Players.Delete;

internal sealed class DeletePlayerCommandHandler(IApplicationDbContext context)
	: ICommandHandler<DeletePlayerCommand>
{
	public async Task<Result> Handle(DeletePlayerCommand command, CancellationToken cancellationToken)
	{
		Player? player = await context.Players
			.FirstOrDefaultAsync(p => p.Id == command.PlayerId, cancellationToken);

		if (player is null)
		{
			return Result.Failure(PlayerErrors.NotFound());
		}

		context.Players.Remove(player);

		await context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}