using Application.Abstractions.Messaging;
using Application.Players.GetById;

namespace Application.Players.GetAll;

public sealed record GetAllPlayersQuery() : IQuery<List<PlayerResponse>>;
