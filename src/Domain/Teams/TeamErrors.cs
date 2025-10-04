using SharedKernel;

namespace Domain.Teams;

public static class TeamErrors
{
	public static Error NotFound()
	{
		return Error.NotFound(
			"Team.NotFound",
			"The team could not be found.");
	}
}