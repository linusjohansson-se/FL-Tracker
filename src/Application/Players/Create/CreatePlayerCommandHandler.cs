using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Players;
using SharedKernel;

namespace Application.Players.Create;

internal sealed class CreatePlayerCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreatePlayerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
    {
        var player = Player.Create(
            command.TeamId,
            command.SteamId,
            command.Nickname,
            command.LeetifyId,
            command.FaceitId,
            dateTimeProvider.UtcNow,
            dateTimeProvider.UtcNow
        );

        context.Players.Add(player);

        await context.SaveChangesAsync(cancellationToken);

        return player.Id;
    }
}
