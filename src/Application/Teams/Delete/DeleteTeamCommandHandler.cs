using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Teams;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Teams.Delete;

internal sealed class DeleteTeamCommandHandler(IApplicationDbContext context)
	: ICommandHandler<DeleteTeamCommand>
{
	public async Task<Result> Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
	{
		Team? team = await context.Teams
			.FirstOrDefaultAsync(t => t.Id == command.TeamId, cancellationToken);

		if (team is null)
		{
			return Result.Failure(TeamErrors.NotFound());
		}

		context.Teams.Remove(team);

		await context.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}