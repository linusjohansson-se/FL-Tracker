using SharedKernel;

namespace Domain.GameStats;

public class GameStat : Entity
{
    private GameStat(Guid playerId, int kills, int assists, int deaths, int adr, int hsRatio, int clutchRatio, int fkpr, int fdpr, DateTime createdAt, DateTime updatedAt)
    {
        Id = Guid.NewGuid();
        PlayerId = playerId;
        Kills = kills;
        Assists = assists;
        Deaths = deaths;
        Adr = adr;
        HsRatio = hsRatio;
        ClutchRatio = clutchRatio;
        Fkpr = fkpr;
        Fdpr = fdpr;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static GameStat Create(Guid playerId, int kills, int assists, int deaths, int adr, int hsRatio, int clutchRatio, int fkpr, int fdpr, DateTime createdAt, DateTime updatedAt)
    {
        return new GameStat(playerId, kills, assists, deaths, adr, hsRatio, clutchRatio, fkpr, fdpr, createdAt, updatedAt);
    }

    public Guid Id { get; private set; }

    public Guid PlayerId { get; private set; }

    public int Kills { get; private set; }

    public int Assists { get; private set; }

    public int Deaths { get; private set; }

    public int Adr { get; private set; }

    public int HsRatio { get; private set; }

    public int ClutchRatio { get; private set; }

    public int Fkpr { get; private set; }

    public int Fdpr { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public void Update(int kills, int assists, int deaths, int adr, int hsRatio, int clutchRatio, int fkpr, int fdpr, DateTime updatedAt)
    {
        Kills = kills;
        Assists = assists;
        Deaths = deaths;
        Adr = adr;
        HsRatio = hsRatio;
        ClutchRatio = clutchRatio;
        Fkpr = fkpr;
        Fdpr = fdpr;
        UpdatedAt = updatedAt;
    }
}
