using System.Collections.Concurrent;
using SharpRoyale.Entities;

namespace SharpRoyale.Services;

public class MatchService
{
    private readonly ConcurrentDictionary<int, Match> _matches = new();

    public bool CreateMatch(Player p1, Player p2)
    {
        throw new NotImplementedException();
    }

    // Not A Valid Overload Situation, But I Didn't Decide Which One To Use Yet
    public bool CreateMatch((Player p1, Player p2) match)
    {
        throw new NotImplementedException();
    }
    
}