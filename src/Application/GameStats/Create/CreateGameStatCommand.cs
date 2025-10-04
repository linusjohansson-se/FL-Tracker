using Application.Abstractions.Messaging;

namespace Application.GameStats.Create;

public sealed class CreateGameStatCommand : ICommand<Guid>
{
    public Guid PlayerId { get; set; }
    public int Kills { get; set; }
    public int Assists { get; set; }
    public int Deaths { get; set; }
    public int Adr { get; set; }
    public int HsRatio { get; set; }
    public double ClutchRatio { get; set; }
    public int Fkpr { get; set; }
    public int Fdpr { get; set; }
    public int RoundsPlayed { get; set; }
    public int TotalDamage { get; set; }
    public int HeadshotKills { get; set; }
    public int FirstKills { get; set; }
    public int FirstDeaths { get; set; }
    public int ClutchAttempts { get; set; }
    public int ClutchWins { get; set; }
}
