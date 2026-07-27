using System.Collections.Concurrent;
using Core.SharpRoyale.GameServices.ActionListService;
using Core.SharpRoyale.GameServices.UserInteractionService;
using Engine.SharpRoyale;

namespace Web.SharpRoyale.Services;

public class MatchService(GameEngine engine)
{
    public ConcurrentDictionary<int, Match> _matches = new();
    private int _counter;

    public int CreateMatch((Player p1, Player p2) match)
    {
        var nextMatchId = Interlocked.Increment(ref _counter);

        var newMatch = new Match(matchId: nextMatchId, players: match);
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

    public bool SendPlayerActionToEngine(
        int matchId,
        int playerid,
        UserInteractionOption action,
        object values
    )
    {
        engine.AppendUserInteractionList(
            new UserInteractionElement(GetMatchFromId(matchId), playerid, action, values)
        );
        // TODO: Needs to be a Result instead
        return true;
    }
}
