using SharedKernel;

namespace Domain.Players;

public class Player : Entity
{
    private Player(Guid? teamId, string steamId, string nickname, string? leetifyId, string? faceitId, DateTime createdAt, DateTime updatedAt)
    {
        Id = Guid.NewGuid();
        TeamId = teamId;
        SteamId = steamId;
        Nickname = nickname;
        LeetifyId = leetifyId;
        FaceitId = faceitId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Player Create(Guid? teamId, string steamId, string nickname, string? leetifyId, string? faceitId, DateTime createdAt, DateTime updatedAt)
    {
        return new Player(teamId, steamId, nickname, leetifyId, faceitId, createdAt, updatedAt);
    }

    public Guid Id { get; private set; }

    public string SteamId { get; private set; }

    public string Nickname { get; private set; }

    public Guid? TeamId { get; private set; }

    public string? LeetifyId { get; private set; }

    public string? FaceitId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public void Update(string nickname, Guid? teamId, string? leetifyId, string? faceitId, DateTime updatedAt)
    {
        Nickname = nickname;
        TeamId = teamId;
        LeetifyId = leetifyId;
        FaceitId = faceitId;
        UpdatedAt = updatedAt;
    }
}
