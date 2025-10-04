using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Teams;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Teams.GetById;

internal sealed class GetTeamByIdQueryHandler(IApplicationDbContext context)
	: IQueryHandler<GetTeamByIdQuery, TeamResponse>
{
	public async Task<Result<TeamResponse>> Handle(GetTeamByIdQuery query, CancellationToken cancellationToken)
	{
		TeamResponse? team = await context.Teams
			.Where(t => t.Id == query.TeamId)
			.Select(t => new TeamResponse
			{
				Id = t.Id, TeamName = t.TeamName, CreatedAt = t.CreatedAt, UpdatedAt = t.UpdatedAt
			})
			.FirstOrDefaultAsync(cancellationToken);

		if (team is null)
		{
			return Result.Failure<TeamResponse>(TeamErrors.NotFound());
		}

		return team;
	}
}