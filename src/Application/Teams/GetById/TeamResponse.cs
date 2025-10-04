namespace Application.Teams.GetById;

public sealed class TeamResponse
{
    public Guid Id { get; set; }
    public string TeamName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
