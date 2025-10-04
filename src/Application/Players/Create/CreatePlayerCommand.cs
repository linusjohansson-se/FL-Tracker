using Application.Abstractions.Messaging;

namespace Application.Players.Create;

public sealed class CreatePlayerCommand : ICommand<Guid>
{
    public Guid? TeamId { get; set; }
    public string SteamId { get; set; }
    public string Nickname { get; set; }
    public string? LeetifyId { get; set; }
    public string? FaceitId { get; set; }
}
