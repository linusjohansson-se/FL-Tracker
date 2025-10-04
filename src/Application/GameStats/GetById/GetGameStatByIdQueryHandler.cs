using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.GameStats;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.GameStats.GetById;

internal sealed class GetGameStatByIdQueryHandler(IApplicationDbContext context)
	: IQueryHandler<GetGameStatByIdQuery, GameStatResponse>
{
	public async Task<Result<GameStatResponse>> Handle(GetGameStatByIdQuery query, CancellationToken cancellationToken)
	{
		GameStatResponse? gameStat = await context.GameStats
			.Where(gs => gs.Id == query.GameStatId)
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
			.FirstOrDefaultAsync(cancellationToken);

		if (gameStat is null)
		{
			return Result.Failure<GameStatResponse>(GameStatErrors.NotFound());
		}

		return gameStat;
	}
}