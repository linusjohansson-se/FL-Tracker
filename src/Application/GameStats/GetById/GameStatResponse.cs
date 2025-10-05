namespace Application.GameStats.GetById;

public sealed class GameStatResponse
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public Guid GameId { get; set; }
    public int Kills { get; set; }
    public int Assists { get; set; }
    public int Deaths { get; set; }
    public double Adr { get; set; }
    public double HsRatio { get; set; }
    public double ClutchRatio { get; set; }
    public double Fkpr { get; set; }
    public double Fdpr { get; set; }
    public int RoundsPlayed { get; set; }
    public int TotalDamage { get; set; }
    public int HeadshotKills { get; set; }
    public int FirstKills { get; set; }
    public int FirstDeaths { get; set; }
    public int ClutchAttempts { get; set; }
    public int ClutchWins { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
