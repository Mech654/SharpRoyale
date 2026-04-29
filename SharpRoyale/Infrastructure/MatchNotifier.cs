using System.Collections.Concurrent;

namespace SharpRoyale.Infrastructure;

public class MatchNotifier
{
    private readonly ConcurrentDictionary<string, HttpResponse> _clients = new();

    public void Register(string playerId, HttpResponse response)
    {
        _clients[playerId] = response;
    }

    public void Unregister(string playerId)
    {
        _clients.TryRemove(playerId, out _);
    }

    public async Task NotifyMatch(string p1, string p2)
    {
        if (_clients.TryGetValue(p1, out var r1))
            await WriteSse(r1, $"Matched with {p2}");

        if (_clients.TryGetValue(p2, out var r2))
            await WriteSse(r2, $"Matched with {p1}");
    }

    private async Task WriteSse(HttpResponse res, string msg)
    {
        await res.WriteAsync($"data: {msg}\n\n");
        await res.Body.FlushAsync();
    }
}