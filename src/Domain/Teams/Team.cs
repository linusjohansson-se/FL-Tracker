using SharedKernel;

namespace Domain.Teams;

public class Team : Entity
{
    private Team(string teamName, DateTime createdAt, DateTime updatedAt)
    {
        Id = Guid.NewGuid();
        TeamName = teamName;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Team Create(string teamName, DateTime createdAt, DateTime updatedAt)
    {
        return new Team(teamName, createdAt, updatedAt);
    }

    public Guid Id { get; private set; }

    public string TeamName { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public void Update(string teamName, DateTime updatedAt)
    {
        TeamName = teamName;
        UpdatedAt = updatedAt;
    }
}
