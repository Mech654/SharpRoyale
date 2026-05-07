namespace Web.SharpRoyale.Infrastructure;

public class MatchmakingWorker(LobbyService lobbyService, MatchNotifier notifier, MatchService matchService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (lobbyService.TryMatching(out var match) && match is (var p1, var p2))
            {
                var matchId = matchService.CreateMatch((p1, p2));
                await notifier.NotifyMatch(p1, p2, matchId);
                notifier.SignalMatchFound(p1, p2);
                Console.WriteLine($"{p1.PlayerId} and {p2.PlayerId}");
            }

            await Task.Delay(200, stoppingToken);
        }
    }
}
