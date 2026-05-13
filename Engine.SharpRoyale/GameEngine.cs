using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Engine.SharpRoyale;

public class GameEngine(ConcurrentDictionary<int, Match> Matches)
{
    
    public async Task RunGameLoop()
    {
        const int tickRate = 60; 
        const int msPerTick = 1000 / tickRate;
        
        while (true)
        {
            var sw = Stopwatch.StartNew();
            
            
            // Sleep
            var elapsed = sw.ElapsedMilliseconds;
            if (elapsed < msPerTick)
                await Task.Delay((int)(msPerTick - elapsed));
        }
    }
}