using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Players;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Players.GetById;

internal sealed class GetPlayerByIdQueryHandler(IApplicationDbContext context)
	: IQueryHandler<GetPlayerByIdQuery, PlayerResponse>
{
	public async Task<Result<PlayerResponse>> Handle(GetPlayerByIdQuery query, CancellationToken cancellationToken)
	{
		PlayerResponse? player = await context.Players
			.Where(p => p.Id == query.PlayerId)
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