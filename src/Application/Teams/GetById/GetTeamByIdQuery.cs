using Application.Abstractions.Messaging;

namespace Application.Teams.GetById;

public sealed record GetTeamByIdQuery(Guid TeamId) : IQuery<TeamResponse>;
