using Application.Abstractions.Messaging;

namespace Application.Games.Update;

public sealed class UpdateGameCommand : ICommand
{
    public Guid GameId { get; set; }
    public DateTime Date { get; set; }
    public int TeamAScore { get; set; }
    public int TeamBScore { get; set; }
    public Guid? Winner { get; set; }
    public string MatchId { get; set; }
}
