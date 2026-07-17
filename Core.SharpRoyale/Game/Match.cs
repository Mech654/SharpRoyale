namespace Core.SharpRoyale;

public class Match
{
    public int MatchId { get; }
    public (Player p1, Player p2) Players { get; }
    public ArenaMap Map { get; }
    public List<GameServices.ActionListService.ActionElement> ActionList { get; } = [];
    private int NextEntityId = 0;

    public Match(int matchId, (Player p1, Player p2) players)
    {
        MatchId = matchId;
        Players = players;
        Map = new ArenaMap().AddPlayerTowers(players, this);
    }

    public int GetNextEntityId()
    {
        NextEntityId++;
        return NextEntityId - 1;
    }
}
