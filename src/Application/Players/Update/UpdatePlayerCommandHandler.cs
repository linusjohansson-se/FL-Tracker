using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Players;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Players.Update;

internal sealed class UpdatePlayerCommandHandler(
	IApplicationDbContext context,
	IDateTimeProvider dateTimeProvider)
	: ICommandHandler<UpdatePlayerCommand>
{
	public async Task<Result> Handle(UpdatePlayerCommand command, CancellationToken cancellationToken)
	{
		Player? player = await context.Players
			.FirstOrDefaultAsync(p => p.Id == command.PlayerId, cancellationToken);

		if (player is null)
		{
			return Result.Failure(PlayerErrors.NotFound());
		}

		player.Update(
			command.Nickname,
			command.TeamId,
			command.LeetifyId,
			command.FaceitId,
			dateTimeProvider.UtcNow
		);

		await context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}