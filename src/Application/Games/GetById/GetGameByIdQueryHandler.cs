using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Games;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Games.GetById;

internal sealed class GetGameByIdQueryHandler(IApplicationDbContext context)
	: IQueryHandler<GetGameByIdQuery, GameResponse>
{
	public async Task<Result<GameResponse>> Handle(GetGameByIdQuery query, CancellationToken cancellationToken)
	{
		GameResponse? game = await context.Games
			.Where(g => g.Id == query.GameId)
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
				MatchId = g.MatchId,
				CreatedAt = g.CreatedAt,
				UpdatedAt = g.UpdatedAt
			})
			.FirstOrDefaultAsync(cancellationToken);

		if (game is null)
		{
			return Result.Failure<GameResponse>(GameErrors.NotFound());
		}

		return game;
	}
}