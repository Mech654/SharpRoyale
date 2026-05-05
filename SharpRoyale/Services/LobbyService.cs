using System.Collections.Concurrent;
using SharpRoyale.Entities;
using SharpRoyale.Infrastructure;

namespace SharpRoyale.Services;

public class LobbyService
{
    private readonly ConcurrentQueue<int> _queue = new();
    private readonly Lock _lock = new();
    
    public void JoinQueue(int playerId)
    {
        _queue.Enqueue(playerId);
        
    }

    public void RemoveFromQueue(int playerId)
    {
        _queue.TryDequeue(out _);
        
    }
    // TODO: Implement Better Matchmaking Logic, Elo Based.
    public bool TryMatching(out (Player p1, Player p2)? match)
    {
        lock (_lock)
        {
            
            match = null;

            if (_queue.Count < 2)
                return false;

            if (_queue.TryDequeue(out var p1) &&
                _queue.TryDequeue(out var p2))
            {
                match = (DbHelper.GetPlayerFromId(p1), DbHelper.GetPlayerFromId(p2));
                return true;
            }
        }

        return false;
    }
    
}