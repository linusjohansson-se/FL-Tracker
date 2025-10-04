using Application.Abstractions.Messaging;

namespace Application.Teams.Create;

public sealed class CreateTeamCommand : ICommand<Guid>
{
    public string TeamName { get; set; }
}
