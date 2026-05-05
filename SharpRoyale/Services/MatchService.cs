using System.Collections.Concurrent;
using SharpRoyale.Entities;

namespace SharpRoyale.Services;

public class MatchService
{
    private ConcurrentDictionary<int, Match> _matches = new();
    private int _counter;

    public bool CreateMatch(Player p1, Player p2)
    {
        throw new NotImplementedException();
    }

    // Not A Valid Overload Situation, But I Didn't Decide Which One To Use Yet
    public int CreateMatch((Player p1, Player p2) match)
    {
        var nextMatchId = Interlocked.Increment(ref _counter);
        
        //TODO: Store this shi somewhere 
        new Match(
            matchId: nextMatchId,
            players: match
        );

        return nextMatchId;
    }
}