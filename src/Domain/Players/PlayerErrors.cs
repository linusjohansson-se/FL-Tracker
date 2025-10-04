using SharedKernel;

namespace Domain.Players;

public static class PlayerErrors
{
	public static Error NotFound()
	{
		return Error.NotFound(
			"Player.NotFound",
			"The player could not be found.");
	}
}