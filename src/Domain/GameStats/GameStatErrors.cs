using SharedKernel;

namespace Domain.GameStats;

public static class GameStatErrors
{
	public static Error NotFound()
	{
		return Error.NotFound(
			"GameStat.NotFound",
			"The game stat could not be found.");
	}
}