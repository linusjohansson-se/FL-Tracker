using Application.Abstractions.Messaging;

namespace Application.Games.Create;

public sealed class CreateGameCommand : ICommand<Guid>
{
    public DateTime Date { get; set; }
    public Guid? TeamA { get; set; }
    public Guid? TeamB { get; set; }
    public string TeamAName { get; set; }
    public string TeamBName { get; set; }
    public int TeamAScore { get; set; }
    public int TeamBScore { get; set; }
    public Guid? Winner { get; set; }
    public string MatchId { get; set; }
}
