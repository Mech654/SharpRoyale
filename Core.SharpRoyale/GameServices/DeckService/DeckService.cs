using Core.SharpRoyale.Entities;

namespace Core.SharpRoyale.GameServices.DeckService;

public static class DeckService
{
    public static int[] GetPlayerDeck(int playerId)
    {
        // Imagine DB call here
        return [2, 3, 4, 5, 6, 7, 8, 9]; // Entity ID
    }

    public static void DeployPlayerDeck(Player player, Match match)
    {
        player.Deck = new Deck(GetPlayerDeck(player.Id))
        {
            PlayerId = player.Id,
            AssociatedMatch = match,
        };
    }

    public static Deck ShuffleDeck(Deck deck)
    { /*
        // Select random entities
        int[] numbers = Enumerable.Range(0, 9).OrderBy(_ => Random.Shared.Next()).Take(4).ToArray();

        for (int i = 0; i < 4; i++)
        {
            deck.AvailableEntities[i] = GetEntityFromId(
                deck.AllEntities[i],
                deck.PlayerId,
                deck.AssociatedMatch
            );
        }

        return deck;
        */ //TODO: Fix this (low priority), issue happens because AvaibleEnities now contain types directly
        throw new NotImplementedException();
    }

    public static Entity GetEntityFromId(int id, int playerId, Match match) // TODO: looking at this again it looks smelly
    {
        return id switch
        {
            1 => new Tower(playerId, match),
            2 => new King(playerId, match),
            3 => new Larry(playerId, match),
            _ => throw new Exception("Unkown id"),
        };
    }
}
