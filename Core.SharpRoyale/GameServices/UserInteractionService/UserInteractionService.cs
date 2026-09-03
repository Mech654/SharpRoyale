using Core.SharpRoyale.GameServices.ActionListService;
using Core.SharpRoyale.GameServices.SpawnService;

namespace Core.SharpRoyale.GameServices.UserInteractionService;

public static class UserInteractionService
{
    public static void AppendUserInteraction(UserInteractionElement userInteractionElement)
    {
        switch (userInteractionElement.action)
        {
            case UserInteractionOption.Spawn:
                AppendUserSpawnAction(userInteractionElement);
                break;
        }
    }

    private static void AppendUserSpawnAction(UserInteractionElement userInteractionElement)
    {
        int? entityId = userInteractionElement.values.GetProperty("entityId").GetInt32();
        if (entityId is null)
            return;

        Match match = userInteractionElement.match;
        Player player = match.GetPlayerFromId(userInteractionElement.playerid);
        Entity entity = DeckService.DeckService.GetEntityFromId(entityId.Value, player.Id, match);

        var position = userInteractionElement.values.GetProperty("Position");

        ActionListService.ActionListService.AppendActionListSpawn(
            new ActionListValueSpawn(
                new Position(
                    position.GetProperty("x").GetDouble(),
                    position.GetProperty("y").GetDouble()
                ),
                userInteractionElement.values.GetProperty("entityId").GetInt32(),
                player
            ),
            match
        );
    }
}
