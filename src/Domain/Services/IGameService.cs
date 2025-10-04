namespace Domain.Services;

public interface IGameService
{
	Task ProcessGameAsync(FileStream file);
}