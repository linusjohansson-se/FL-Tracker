using Application.Abstractions.Messaging;

namespace Application.Teams.Delete;

public sealed record DeleteTeamCommand(Guid TeamId) : ICommand;
