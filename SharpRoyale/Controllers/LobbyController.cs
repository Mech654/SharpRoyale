using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SharpRoyale.Controllers;

[Authorize]
[Route("api/lobby")]
public class LobbyController(LobbyService lobbyService, MatchNotifier matchNotifier) : ControllerBase
{
    [HttpGet("stream/join")]
    public async Task JoinQue()
    {
        Response.Headers.Append("Content-Type", "text/event-stream");

        var playerId = "123"; // Change To JWT Extraction Later
        matchNotifier.Register(playerId, Response);
        lobbyService.JoinQueue(playerId);
        
        try
        {
            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                await Response.WriteAsync(":\n\n");
                await Response.Body.FlushAsync();
                await Task.Delay(TimeSpan.FromSeconds(10), HttpContext.RequestAborted);
            }
        }
        catch (OperationCanceledException)
        {
            // client disconnected
        }
        finally
        {
            matchNotifier.Unregister(playerId);
            lobbyService.RemoveFromQueue(playerId);
        }
    }

    [HttpGet("exit")]
    public IActionResult ExitQue()
    {
        return Ok();
    }
}