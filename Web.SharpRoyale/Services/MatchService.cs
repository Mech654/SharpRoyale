using System.Collections.Concurrent;
using Engine.SharpRoyale;
using Core.SharpRoyale.GameServices.ActionListService;
using Core.SharpRoyale.GameServices.UserInteractionService;

namespace Web.SharpRoyale.Services;

public class MatchService (GameEngine engine)
{
    public ConcurrentDictionary<int, Match> _matches = new();
    private int _counter;

    public int CreateMatch((Player p1, Player p2) match)
    {
        var nextMatchId = Interlocked.Increment(ref _counter);

        var newMatch = new Match(
            matchId: nextMatchId,
            players: match
        );
        _matches.TryAdd(nextMatchId, newMatch);
        
        _ = engine.RunGameLoop(newMatch);

        return nextMatchId;
    }

    public bool CheckMatchExists(int matchId)
    {
        return _matches.ContainsKey(matchId);
    }

    public Match GetMatchFromId(int matchId)
    {
        return _matches[matchId];
    }

    public bool SendPlayerActionToEngine(int matchId, Player player, UserInteractionOption action, object values)
    {
        engine.AppendUserInteractionList(new UserInteractionElement(GetMatchFromId(matchId), player, action, values));
        // TODO: Needs to be a Result instead
        return true;
    }
}
