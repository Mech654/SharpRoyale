using System.Collections.Concurrent;
using Web.SharpRoyale.Entities;

namespace Web.SharpRoyale.Services;

public class MatchService
{
    private ConcurrentDictionary<int, Match> _matches = new();
    private int _counter;

    public int CreateMatch((Player p1, Player p2) match)
    {
        var nextMatchId = Interlocked.Increment(ref _counter);

        var newMatch = new Match(
            matchId: nextMatchId,
            players: match
        );
        _matches.TryAdd(nextMatchId, newMatch);

        return nextMatchId;
    }

    public bool CheckMatchExists(int matchId)
    {
        return _matches.ContainsKey(matchId);
    }
}
