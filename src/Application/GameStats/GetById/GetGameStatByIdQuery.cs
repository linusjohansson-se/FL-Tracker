using Application.Abstractions.Messaging;

namespace Application.GameStats.GetById;

public sealed record GetGameStatByIdQuery(Guid GameStatId) : IQuery<GameStatResponse>;
