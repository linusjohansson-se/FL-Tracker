using Application.Abstractions.Messaging;

namespace Application.Players.Update;

public sealed class UpdatePlayerCommand : ICommand
{
    public Guid PlayerId { get; set; }
    public string Nickname { get; set; }
    public Guid? TeamId { get; set; }
    public string? LeetifyId { get; set; }
    public string? FaceitId { get; set; }
}
