using Core.SharpRoyale.Entities;

namespace Core.SharpRoyale;

public class Deck(int[] allEntities)
{
    public required int PlayerId;
    public required Match AssociatedMatch;
    public int[] AllEntities { get; } = allEntities;
    public int[] AvailableEntities = { };
    public List<Entity> UnavailablePool = new List<Entity>();
}
