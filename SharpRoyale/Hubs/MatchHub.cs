using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SharpRoyale.Hubs;

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
    
}