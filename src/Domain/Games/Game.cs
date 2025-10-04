using SharedKernel;

namespace Domain.Games;

public class Game : Entity
{
    private Game(DateTime date, Guid teamA, Guid teamB, string teamAName, string teamBName, int teamAScore, int teamBScore, Guid winner, DateTime createdAt, DateTime updatedAt)
    {
        Id = Guid.NewGuid();
        Date = date;
        TeamA = teamA;
        TeamB = teamB;
        TeamAName = teamAName;
        TeamBName = teamBName;
        TeamAScore = teamAScore;
        TeamBScore = teamBScore;
        Winner = winner;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Game Create(DateTime date, Guid teamA, Guid teamB, string teamAName, string teamBName, int teamAScore, int teamBScore, Guid winner, DateTime createdAt, DateTime updatedAt)
    {
        return new Game(date, teamA, teamB, teamAName, teamBName, teamAScore, teamBScore, winner, createdAt, updatedAt);
    }

    public Guid Id { get; private set; }

    public DateTime Date { get; private set; }

    public Guid TeamA { get; private set; }

    public Guid TeamB { get; private set; }

    public string TeamAName { get; private set; }

    public string TeamBName { get; private set; }

    public int TeamAScore { get; private set; }

    public int TeamBScore { get; private set; }

    public Guid Winner { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public void Update(DateTime date, int teamAScore, int teamBScore, Guid winner, DateTime updatedAt)
    {
        Date = date;
        TeamAScore = teamAScore;
        TeamBScore = teamBScore;
        Winner = winner;
        UpdatedAt = updatedAt;
    }
}
