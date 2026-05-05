using System.Collections.Concurrent;

namespace SharpRoyale.Infrastructure;

public class MatchNotifier
{
    private readonly ConcurrentDictionary<int, HttpResponse> _clients = new();

    public void Register(int playerId, HttpResponse response)
    {
        _clients[playerId] = response;
    }

    public void Unregister(int playerId)
    {
        _clients.TryRemove(playerId, out _);
    }

    public async Task NotifyMatch(Player p1, Player p2)
    {
        if (_clients.TryGetValue(p1.PlayerId, out var r1))
            await WriteSse(r1, $"Matched with {p2}");

        if (_clients.TryGetValue(p2.PlayerId, out var r2))
            await WriteSse(r2, $"Matched with {p1}");
    }

    private async Task WriteSse(HttpResponse res, string msg)
    {
        await res.WriteAsync($"data: {msg}\n\n");
        await res.Body.FlushAsync();
    }
}