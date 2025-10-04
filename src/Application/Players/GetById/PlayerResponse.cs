namespace Application.Players.GetById;

public sealed class PlayerResponse
{
    public Guid Id { get; set; }
    public Guid? TeamId { get; set; }
    public string SteamId { get; set; }
    public string Nickname { get; set; }
    public string? LeetifyId { get; set; }
    public string? FaceitId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
