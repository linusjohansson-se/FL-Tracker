using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Games.GetById;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Games.GetAll;

internal sealed class GetAllGamesQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetAllGamesQuery, List<GameResponse>>
{
    public async Task<Result<List<GameResponse>>> Handle(GetAllGamesQuery query, CancellationToken cancellationToken)
    {
        var games = await context.Games
            .Select(g => new GameResponse
            {
                Id = g.Id,
                Date = g.Date,
                TeamA = g.TeamA,
                TeamB = g.TeamB,
                TeamAName = g.TeamAName,
                TeamBName = g.TeamBName,
                TeamAScore = g.TeamAScore,
                TeamBScore = g.TeamBScore,
                Winner = g.Winner,
                CreatedAt = g.CreatedAt,
                UpdatedAt = g.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return games;
    }
}
