using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Players.GetById;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Players.GetAll;

internal sealed class GetAllPlayersQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetAllPlayersQuery, List<PlayerResponse>>
{
    public async Task<Result<List<PlayerResponse>>> Handle(GetAllPlayersQuery query, CancellationToken cancellationToken)
    {
        var players = await context.Players
            .Select(p => new PlayerResponse
            {
                Id = p.Id,
                TeamId = p.TeamId,
                SteamId = p.SteamId,
                Nickname = p.Nickname,
                LeetifyId = p.LeetifyId,
                FaceitId = p.FaceitId,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return players;
    }
}
