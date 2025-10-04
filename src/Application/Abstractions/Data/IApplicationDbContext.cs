using Domain.Games;
using Domain.GameStats;
using Domain.Players;
using Domain.Teams;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
	DbSet<Player> Players { get; }
	DbSet<Team> Teams { get; }
	DbSet<Game> Games { get; }
	DbSet<GameStat> GameStats { get; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}