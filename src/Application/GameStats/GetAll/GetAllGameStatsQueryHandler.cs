using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.GameStats.GetById;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.GameStats.GetAll;

internal sealed class GetAllGameStatsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetAllGameStatsQuery, List<GameStatResponse>>
{
    public async Task<Result<List<GameStatResponse>>> Handle(GetAllGameStatsQuery query, CancellationToken cancellationToken)
    {
        var gameStats = await context.GameStats
            .Select(gs => new GameStatResponse
            {
                Id = gs.Id,
                PlayerId = gs.PlayerId,
                Kills = gs.Kills,
                Assists = gs.Assists,
                Deaths = gs.Deaths,
                Adr = gs.Adr,
                HsRatio = gs.HsRatio,
                ClutchRatio = gs.ClutchRatio,
                Fkpr = gs.Fkpr,
                Fdpr = gs.Fdpr,
                CreatedAt = gs.CreatedAt,
                UpdatedAt = gs.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return gameStats;
    }
}
