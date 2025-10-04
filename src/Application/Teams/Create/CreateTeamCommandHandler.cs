using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Teams;
using SharedKernel;

namespace Application.Teams.Create;

internal sealed class CreateTeamCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateTeamCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
    {
        var team = Team.Create(
            command.TeamName,
            dateTimeProvider.UtcNow,
            dateTimeProvider.UtcNow
        );

        context.Teams.Add(team);

        await context.SaveChangesAsync(cancellationToken);

        return team.Id;
    }
}
