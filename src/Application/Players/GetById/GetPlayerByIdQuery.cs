using Application.Abstractions.Messaging;

namespace Application.Players.GetById;

public sealed record GetPlayerByIdQuery(Guid PlayerId) : IQuery<PlayerResponse>;
