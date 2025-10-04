using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Games;
using SharedKernel;

namespace Application.Games.Create;

internal sealed class CreateGameCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateGameCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateGameCommand command, CancellationToken cancellationToken)
    {
        var game = Game.Create(
            command.Date,
            command.TeamA,
            command.TeamB,
            command.TeamAName,
            command.TeamBName,
            command.TeamAScore,
            command.TeamBScore,
            command.Winner,
            dateTimeProvider.UtcNow,
            dateTimeProvider.UtcNow
        );

        context.Games.Add(game);

        await context.SaveChangesAsync(cancellationToken);

        return game.Id;
    }
}
