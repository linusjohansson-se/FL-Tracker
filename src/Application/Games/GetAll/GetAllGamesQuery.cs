using Application.Abstractions.Messaging;
using Application.Games.GetById;

namespace Application.Games.GetAll;

public sealed record GetAllGamesQuery() : IQuery<List<GameResponse>>;
