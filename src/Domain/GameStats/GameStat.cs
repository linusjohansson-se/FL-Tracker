using SharedKernel;

namespace Domain.GameStats;

public class GameStat : Entity
{
    private GameStat(Guid playerId, Guid gameId, int kills, int assists, int deaths, double adr, double hsRatio, double clutchRatio, double fkpr, double fdpr, int roundsPlayed, int totalDamage, int headshotKills, int firstKills, int firstDeaths, int clutchAttempts, int clutchWins, DateTime createdAt, DateTime updatedAt)
    {
        Id = Guid.NewGuid();
        PlayerId = playerId;
        GameId = gameId;
        Kills = kills;
        Assists = assists;
        Deaths = deaths;
        Adr = adr;
        HsRatio = hsRatio;
        ClutchRatio = clutchRatio;
        Fkpr = fkpr;
        Fdpr = fdpr;
        RoundsPlayed = roundsPlayed;
        TotalDamage = totalDamage;
        HeadshotKills = headshotKills;
        FirstKills = firstKills;
        FirstDeaths = firstDeaths;
        ClutchAttempts = clutchAttempts;
        ClutchWins = clutchWins;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static GameStat Create(Guid playerId, Guid gameId, int kills, int assists, int deaths, double adr, double hsRatio, double clutchRatio, double fkpr, double fdpr, int roundsPlayed, int totalDamage, int headshotKills, int firstKills, int firstDeaths, int clutchAttempts, int clutchWins, DateTime createdAt, DateTime updatedAt)
    {
        return new GameStat(playerId, gameId, kills, assists, deaths, adr, hsRatio, clutchRatio, fkpr, fdpr, roundsPlayed, totalDamage, headshotKills, firstKills, firstDeaths, clutchAttempts, clutchWins, createdAt, updatedAt);
    }

    public Guid Id { get; private set; }

    public Guid PlayerId { get; private set; }

    public Guid GameId { get; private set; }

    public int Kills { get; private set; }

    public int Assists { get; private set; }

    public int Deaths { get; private set; }

    public double Adr { get; private set; }

    public double HsRatio { get; private set; }

    public double ClutchRatio { get; private set; }

    public double Fkpr { get; private set; }

    public double Fdpr { get; private set; }

    public int RoundsPlayed { get; private set; }

    public int TotalDamage { get; private set; }

    public int HeadshotKills { get; private set; }

    public int FirstKills { get; private set; }

    public int FirstDeaths { get; private set; }

    public int ClutchAttempts { get; private set; }

    public int ClutchWins { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public void Update(int kills, int assists, int deaths, double adr, double hsRatio, double clutchRatio, double fkpr, double fdpr, int roundsPlayed, int totalDamage, int headshotKills, int firstKills, int firstDeaths, int clutchAttempts, int clutchWins, DateTime updatedAt)
    {
        Kills = kills;
        Assists = assists;
        Deaths = deaths;
        Adr = adr;
        HsRatio = hsRatio;
        ClutchRatio = clutchRatio;
        Fkpr = fkpr;
        Fdpr = fdpr;
        RoundsPlayed = roundsPlayed;
        TotalDamage = totalDamage;
        HeadshotKills = headshotKills;
        FirstKills = firstKills;
        FirstDeaths = firstDeaths;
        ClutchAttempts = clutchAttempts;
        ClutchWins = clutchWins;
        UpdatedAt = updatedAt;
    }
}
