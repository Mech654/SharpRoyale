namespace Core.SharpRoyale;

public class Deck(int[] allEntities)
{
    public required int PlayerId;
    public required Match AssociatedMatch;
    public int[] AllEntities { get; } = allEntities;
    public Entity[] AvailableEntities = new Entity[4];
    public List<Entity> UnavailablePool = new List<Entity>();

}