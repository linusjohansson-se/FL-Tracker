using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Players.GetById;
using Domain.Players;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Players.GetBySteamId;

internal sealed class GetPlayerBySteamIdQueryHandler(IApplicationDbContext context)
	: IQueryHandler<GetPlayerBySteamIdQuery, PlayerResponse>
{
	public async Task<Result<PlayerResponse>> Handle(GetPlayerBySteamIdQuery query, CancellationToken cancellationToken)
	{
		PlayerResponse? player = await context.Players
			.Where(p => p.SteamId == query.SteamId)
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
			.FirstOrDefaultAsync(cancellationToken);

		if (player is null)
		{
			return Result.Failure<PlayerResponse>(PlayerErrors.NotFound());
		}

		return player;
	}
}