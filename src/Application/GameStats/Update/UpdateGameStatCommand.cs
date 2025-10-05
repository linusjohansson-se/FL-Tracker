using Application.Abstractions.Messaging;

namespace Application.GameStats.Update;

public sealed class UpdateGameStatCommand : ICommand
{
    public Guid GameStatId { get; set; }
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
}
