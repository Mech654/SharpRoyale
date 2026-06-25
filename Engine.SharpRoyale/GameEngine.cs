using System.Collections.Concurrent;
using System.Diagnostics;
using Core.SharpRoyale;

namespace Engine.SharpRoyale;

public class GameEngine(ConcurrentDictionary<int, Match> matches, TickClientFeedback tickClientFeedback)
{
    public record UserInteractionElement(int matchId, int playerId, UserInteractionOption action, object values);

    public List<UserInteractionElement> UserInteractionList = [];

    public void AppendUserInteractionList(UserInteractionElement userInteraction)
    {
        //TODO: Validation Logic For The UserAction, Examples: Spawn Location(Tile), Elixir Capacity, Card Available In Deck
        var time = DateTime.Now;
        UserInteractionList.Add(userInteraction);
    }

    public void DeployPlayerDecks(Player player1, Player player2, Match match)
    {
        DeckService.DeployPlayerDeck(player1, match);
        DeckService.DeployPlayerDeck(player2, match);
        // TODO: Probably need feedback to user here
    }
    public async Task RunGameLoop(Match m)
    {
        const int tickRate = 60; 
        const int msPerTick = 1000 / tickRate;
        

        DeployPlayerDecks(m.Players.p1, m.Players.p2, m);

        while (true)
        {
            var sw = Stopwatch.StartNew();

            // Apply User Actions
            foreach (UserInteractionElement userInteraction in UserInteractionList)
            {
                SpawnService.SpawnSingularEntity(userInteraction);
            }

            // Collect Action
            foreach (Entity entity in m.Map.Entities)
            {
                entity.Tick();
            }
            
            // Apply Action
            
            // Sleep
            var elapsed = sw.ElapsedMilliseconds;
            if (elapsed < msPerTick)
                await Task.Delay((int)(msPerTick - elapsed));
        }
    }
}