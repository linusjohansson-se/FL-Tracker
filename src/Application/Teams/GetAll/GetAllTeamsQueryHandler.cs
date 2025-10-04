using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Teams.GetById;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Teams.GetAll;

internal sealed class GetAllTeamsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetAllTeamsQuery, List<TeamResponse>>
{
    public async Task<Result<List<TeamResponse>>> Handle(GetAllTeamsQuery query, CancellationToken cancellationToken)
    {
        var teams = await context.Teams
            .Select(t => new TeamResponse
            {
                Id = t.Id,
                TeamName = t.TeamName,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return teams;
    }
}
