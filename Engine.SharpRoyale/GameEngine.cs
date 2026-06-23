using System.Collections.Concurrent;
using System.Diagnostics;
using Core.SharpRoyale;

namespace Engine.SharpRoyale;

public class GameEngine(ConcurrentDictionary<int, Match> Matches)
{
    public record UserInteractionElement(int matchId, int playerId, UserInteractionOption action, object values);

    public List<UserInteractionElement> UserInteractionList = [];

    public void AppendUserInteractionList(UserInteractionElement userInteraction)
    {
        //TODO: Validation Logic For The UserAction, Examples: Spawn Location(Tile), Elixir Capacity, Card Available In Deck
        var time = DateTime.Now;
        UserInteractionList.Add(userInteraction);
    }
    public async Task RunGameLoop(Match m)
    {
        const int tickRate = 60; 
        const int msPerTick = 1000 / tickRate;
        
        while (true)
        {
            var sw = Stopwatch.StartNew();

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