using Core.SharpRoyale.GameServices.ActionListService;
using Core.SharpRoyale.GameServices.SpawnService;

namespace Core.SharpRoyale.GameServices.UserInteractionService;

public static class UserInteractionService
{
    public static void ApplyUserInteraction(UserInteractionElement userInteractionElement)
    {
        switch (userInteractionElement.action)
        {
            case UserInteractionOption.Spawn:
                ApplyUserSpawnAction(userInteractionElement);
                break;
        }
    }

    private static void ApplyUserSpawnAction(UserInteractionElement userInteractionElement)
    {
        int? entityId = userInteractionElement.values.EntityId;
        if (entityId is null)
            return;

        Match match = userInteractionElement.match;
        Player player = match.GetPlayerFromId(userInteractionElement.playerid);
        Entity entity = DeckService.DeckService.GetEntityFromId(entityId.Value, player.Id, match);

        ActionListService.ActionListService.AppendActionListSpawn(
            new ActionListValueSpawn(
                new Position(0, 0),
                userInteractionElement.values.EntityId,
                player
            ),
            match
        );
    }
}
