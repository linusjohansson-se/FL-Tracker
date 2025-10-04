using Application.Abstractions.Messaging;
using Application.GameStats.GetById;

namespace Application.GameStats.GetAll;

public sealed record GetAllGameStatsQuery() : IQuery<List<GameStatResponse>>;
