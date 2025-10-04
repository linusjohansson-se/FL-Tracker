using Application.Abstractions.Messaging;

namespace Application.Games.GetById;

public sealed record GetGameByIdQuery(Guid GameId) : IQuery<GameResponse>;
