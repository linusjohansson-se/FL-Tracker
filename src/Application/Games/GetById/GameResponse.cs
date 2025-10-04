namespace Application.Games.GetById;

public sealed class GameResponse
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid TeamA { get; set; }
    public Guid TeamB { get; set; }
    public string TeamAName { get; set; }
    public string TeamBName { get; set; }
    public int TeamAScore { get; set; }
    public int TeamBScore { get; set; }
    public Guid Winner { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
