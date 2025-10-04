using Application.Abstractions.Messaging;

namespace Application.Players.GetBySteamId;

public sealed record GetPlayerBySteamIdQuery(string SteamId) : IQuery<PlayerResponse>;