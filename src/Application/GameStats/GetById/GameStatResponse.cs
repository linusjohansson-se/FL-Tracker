namespace Application.GameStats.GetById;

public sealed class GameStatResponse
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public int Kills { get; set; }
    public int Assists { get; set; }
    public int Deaths { get; set; }
    public int Adr { get; set; }
    public int HsRatio { get; set; }
    public int ClutchRatio { get; set; }
    public int Fkpr { get; set; }
    public int Fdpr { get; set; }
    public int RoundsPlayed { get; set; }
    public int TotalDamage { get; set; }
    public int HeadshotKills { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
