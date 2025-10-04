using Application.Abstractions.Messaging;

namespace Application.Games.Delete;

public sealed record DeleteGameCommand(Guid GameId) : ICommand;
