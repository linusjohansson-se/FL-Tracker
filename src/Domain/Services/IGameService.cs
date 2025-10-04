using SharedKernel;

namespace Domain.Services;

public interface IGameService
{
	Task<Result> ProcessGameAsync(FileStream file);
}