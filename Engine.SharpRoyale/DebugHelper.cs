using System.Diagnostics;
namespace Engine.SharpRoyale;

public static class DebugHelper
{
    private static int runtimeLimitSeconds = 60;
    private static Stopwatch stopwatch = new Stopwatch();
    private static bool isDone = false;

    public static void StartTime(int runtimeLimit)
    {
        runtimeLimitSeconds = runtimeLimit;
        stopwatch.Restart();
    }

    public static bool IsOverLimit()
    {
        if (isDone)
            return true;

        if (stopwatch.Elapsed.TotalSeconds >= runtimeLimitSeconds)
        {
            isDone = true;
        }

        return isDone;
    }

    public static double ElapsedSeconds()
    {
        return stopwatch.Elapsed.TotalSeconds;
    }
}
