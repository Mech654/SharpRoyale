namespace SharpRoyale.Infrastructure;

public class MatchmakingWorker(LobbyService lobbyService, MatchNotifier notifier, MatchService matchService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (lobbyService.TryMatching(out var match) && match is (var p1, var p2))
            {
                matchService.CreateMatch((p1, p2));
                await notifier.NotifyMatch(p1, p2);
            }

            await Task.Delay(200, stoppingToken);
        }
    }
}