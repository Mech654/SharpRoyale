namespace SharpRoyale.Infrastructure;

public class MatchmakingWorker(LobbyService lobbyService, MatchNotifier notifier) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (lobbyService.TryGetMatch(out var match) && match != null)
            {
                await notifier.NotifyMatch(match.Value.p1, match.Value.p2);
            }

            await Task.Delay(200, stoppingToken);
        }
    }
}