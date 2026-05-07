using System.Collections.Concurrent;
using System.Text.Json;

namespace Web.SharpRoyale.Infrastructure;

public class MatchNotifier
{
    private readonly ConcurrentDictionary<int, HttpResponse> _clients = new();
    private readonly ConcurrentDictionary<int, TaskCompletionSource<bool>> _waiters = new();
    public void Register(int playerId, HttpResponse response)
    {
        _clients[playerId] = response;
    }

    public void Unregister(int playerId)
    {
        _clients.TryRemove(playerId, out _);
    }

    public async Task NotifyMatch(Player p1, Player p2, int matchId)
    {
        if (_clients.TryGetValue(p1.PlayerId, out var r1))
        {
            var payload = new { type = "matchFound", matchId = matchId };
            await WriteSse(r1, JsonSerializer.Serialize(payload));
        }

        if (_clients.TryGetValue(p2.PlayerId, out var r2))
        {
            var payload = new { type = "matchFound", matchId = matchId };
            await WriteSse(r2, JsonSerializer.Serialize(payload));
        }
    }

    private async Task WriteSse(HttpResponse res, string msg)
    {
        await res.WriteAsync($"data: {msg}\n\n");
        await res.Body.FlushAsync();
    }

    public Task WaitForMatchAsync(int playerId, CancellationToken ct)
    {
        var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        _waiters[playerId] = tcs;

        if (ct.CanBeCanceled)
        {
            ct.Register(() => tcs.TrySetCanceled(ct));
        }

        return tcs.Task;
    }

    public void SignalMatchFound(Player p1, Player p2)
    {
        if (_waiters.TryRemove(p1.PlayerId, out var tcs1))
        {
            tcs1.TrySetResult(true);
        }

        if (_waiters.TryRemove(p2.PlayerId, out var tcs2))
        {
            tcs2.TrySetResult(true);
        }
    }

    public void RemoveWaiter(int playerId)
    {
        _waiters.TryRemove(playerId, out _);
    }
}
