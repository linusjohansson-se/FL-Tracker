using Application.Abstractions.Messaging;

namespace Application.Teams.Update;

public sealed class UpdateTeamCommand : ICommand
{
    public Guid TeamId { get; set; }
    public string TeamName { get; set; }
}
