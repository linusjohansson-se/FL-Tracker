using Domain.Services;
using Quartz;

namespace Application.Jobs;

[DisallowConcurrentExecution]
public class MatchSyncJob : IJob
{
	private readonly IGameService _gameService;

	public MatchSyncJob(IGameService gameService)
	{
		_gameService = gameService;
	}

	public async Task Execute(IJobExecutionContext context)
	{
		string path = "2025-09-25_19-00-25_7_de_ancient_team_Nickepice_vs_team_mydaiodir.dem";
		await _gameService.ProcessGameAsync(File.OpenRead(path)); }
}