using System.Security.Claims;
using Core.SharpRoyale.GameServices.ActionListService;
using Engine.SharpRoyale;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Web.SharpRoyale.Hubs;

[Authorize]
public class MatchHub(MatchService matchService) : Hub
{
    private Player? _player;
    private int? _matchId;

    public override Task OnConnectedAsync()
    {
        _player = DbHelper.GetPlayerFromId(GetPlayerId(Context.User));
        return base.OnConnectedAsync();
    }

    public async Task JoinMatch(int matchId)
    {
        if (matchService.CheckMatchExists(matchId))
        {
            _matchId = matchId;
        }
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
        
        if (userInteractionOption == null) return Result.Fail("Invalid Action Option");
        if (_player == null) return Result.Fail("Player not assigned");
        if (_matchId == null) return Result.Fail("Match not assigned");
        
        matchService.SendPlayerActionToEngine(_matchId.Value, _player.PlayerId, userInteractionOption.Value, values);

        return Result.Ok();
    }

    private UserInteractionOption? MatchUserInteractionOption(string action)
    {
        if (Enum.TryParse<UserInteractionOption>(action, true, out var option))
            return option;

        return null;
    }

}
