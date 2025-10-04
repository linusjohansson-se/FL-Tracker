using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.GameStats;
using SharedKernel;

namespace Application.GameStats.Create;

internal sealed class CreateGameStatCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateGameStatCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateGameStatCommand command, CancellationToken cancellationToken)
    {
        var gameStat = GameStat.Create(
            command.PlayerId,
            command.Kills,
            command.Assists,
            command.Deaths,
            command.Adr,
            command.HsRatio,
            command.ClutchRatio,
            command.Fkpr,
            command.Fdpr,
            command.RoundsPlayed,
            command.TotalDamage,
            command.HeadshotKills,
            dateTimeProvider.UtcNow,
            dateTimeProvider.UtcNow
        );

        context.GameStats.Add(gameStat);

        await context.SaveChangesAsync(cancellationToken);

        return gameStat.Id;
    }
}
