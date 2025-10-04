using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Teams;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Teams.Update;

internal sealed class UpdateTeamCommandHandler(
	IApplicationDbContext context,
	IDateTimeProvider dateTimeProvider)
	: ICommandHandler<UpdateTeamCommand>
{
	public async Task<Result> Handle(UpdateTeamCommand command, CancellationToken cancellationToken)
	{
		Team? team = await context.Teams
			.FirstOrDefaultAsync(t => t.Id == command.TeamId, cancellationToken);

		if (team is null)
		{
			return Result.Failure(TeamErrors.NotFound());
		}

		team.Update(
			command.TeamName,
			dateTimeProvider.UtcNow
		);

		await context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}