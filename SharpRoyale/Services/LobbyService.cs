using System.Collections.Concurrent;

namespace SharpRoyale.Services;

public class LobbyService
{
    private readonly ConcurrentQueue<string> _queue = new();
    private readonly object _lock = new();
    
    public void JoinQueue(string playerId)
    {
        _queue.Enqueue(playerId);
        
    }

    public void RemoveFromQueue(string playerId)
    {
        _queue.TryDequeue(out _);
        
    }
    // TODO: Implement Better Matchmaking Logic, Elo Based.
    public bool TryGetMatch(out (string p1, string p2)? match)
    {
        lock (_lock)
        {
            
            match = null;

            if (_queue.Count < 2)
                return false;

            if (_queue.TryDequeue(out var p1) &&
                _queue.TryDequeue(out var p2))
            {
                match = (p1, p2);
                return true;
            }
        }

        return false;
    }
    
}