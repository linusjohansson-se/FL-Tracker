using SharedKernel;

namespace Domain.Games;

public static class GameErrors
{
	public static Error NotFound()
	{
		return Error.NotFound(
			"Game.NotFound",
			"The game could not be found.");
	}
}