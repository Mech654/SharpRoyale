using System.Security.Claims;
using Core.SharpRoyale.GameServices.UserInteractionService;
using Engine.SharpRoyale;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Web.SharpRoyale.Hubs;

[Authorize]
public class MatchHub(MatchService matchService) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var matchId = GetMatchIdFromRoute(Context.GetHttpContext());
        if (!matchService.CheckMatchExists(matchId))
            throw new HubException("match_not_found");

        await Groups.AddToGroupAsync(Context.ConnectionId, $"match:{matchId}");

        await base.OnConnectedAsync();
    }

    public Task JoinMatch(int matchId)
    {
        if (matchService.CheckMatchExists(matchId))
        {
        }

        return Task.CompletedTask;
    }

    private static int GetMatchIdFromRoute(HttpContext? httpContext)
    {
        var routeValue = httpContext?.Request.RouteValues["matchId"]?.ToString();
        if (!int.TryParse(routeValue, out var matchId))
            throw new HubException("invalid_match_id");

        return matchId;
    }

    private static int GetPlayerId(ClaimsPrincipal? user)
    {
        var idValue = user?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(idValue) || !int.TryParse(idValue, out var playerId))
        {
            throw new HubException("invalid_player_id");
        }

        return playerId;
    }

    public record Result(bool Success, string? Error = null)
    {
        public static Result Ok() => new(true);

        public static Result Fail(string error) => new(false, error);
    }

    public Result SendPlayerAction(string action, object values)
    {
        UserInteractionOption? userInteractionOption = MatchUserInteractionOption(action);

        if (userInteractionOption == null)
        {
            Console.WriteLine("FAILED UI OPTION");
            return Result.Fail("Invalid Action Option");
        }

        if (GetPlayerId(Context.User) <= 0)
        {
            Console.WriteLine($"player is {GetPlayerId(Context.User)}");
            Console.WriteLine("player not assigned");
            return Result.Fail("Player not assigned");
        }
        if (GetMatchIdFromRoute(Context.GetHttpContext()) <= 0)
            return Result.Fail("Match not assigned");

        matchService.SendPlayerActionToEngine(
            GetMatchIdFromRoute(Context.GetHttpContext()),
            GetPlayerId(Context.User),
            userInteractionOption.Value,
            values
        );

        Console.WriteLine("SEND USER ACTION!!!");
        return Result.Ok();
    }

    private UserInteractionOption? MatchUserInteractionOption(string action)
    {
        if (Enum.TryParse<UserInteractionOption>(action, true, out var option))
            return option;

        return null;
    }
}
