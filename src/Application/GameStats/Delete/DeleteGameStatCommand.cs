using Application.Abstractions.Messaging;

namespace Application.GameStats.Delete;

public sealed record DeleteGameStatCommand(Guid GameStatId) : ICommand;
