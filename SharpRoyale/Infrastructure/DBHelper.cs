
namespace SharpRoyale.Infrastructure;

// Mostly mock stuff until Db gets implemented
public static class DbHelper
{
    public static Player GetPlayerFromId(int playerId)
    {
        // Ssh Imagine DB call here...
        if (playerId == 1)
        {
            return new Player( playerId: playerId );
        }
        else
        {
            return new Player(playerId: playerId);
        }
    }

    public static int GetPlayerIdFromUsername(string username)
    {
        return username.Contains('a') ? 1 : 2;
    }
}