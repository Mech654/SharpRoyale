namespace SharpRoyale.Entities;

public class Match
{
    public int MatchId { get; }
    public (Player p1, Player p2) Players { get; }

    public Match(int matchId, (Player p1, Player p2) players)
    {
        MatchId = matchId;
        Players = players;
    }
}