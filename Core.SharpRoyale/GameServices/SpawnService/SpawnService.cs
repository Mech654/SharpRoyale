using Core.SharpRoyale.GameServices.UserInteractionService;

namespace Core.SharpRoyale.GameServices.SpawnService;

public static class SpawnService
{
    public static void SpawnSingularEntity(UserInteractionElement userInteractionElement)
    {
        int? entityId = userInteractionElement.values.Entity;
        if (entityId is null) return;
        
        Player player = userInteractionElement.player;
        Match match = userInteractionElement.match;
        
        Entity entity = DeckService.DeckService.GetEntityFromId(entityId.Value, player.Id, match);
        
        if (!player.Deck.AvailableEntities.Contains(entity)) return;
        if (!match.Map.CheckIfEntityCanBeDeployed(entity)) return;
        if (!(player.Elixir >= entity.ElixirCost)) return;

        userInteractionElement.match.Map.Entities.Add(entity);
    }
}
