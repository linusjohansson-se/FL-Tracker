using Application.Abstractions.Messaging;

namespace Application.Players.Delete;

public sealed record DeletePlayerCommand(Guid PlayerId) : ICommand;
