using Application.Abstractions.Messaging;
using Application.Teams.GetById;

namespace Application.Teams.GetAll;

public sealed record GetAllTeamsQuery() : IQuery<List<TeamResponse>>;
