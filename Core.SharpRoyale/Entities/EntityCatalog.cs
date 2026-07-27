namespace Core.SharpRoyale.Entities;

public static class EntityCatalog
{
    public static (int Width, int Height) GetSize(int entityId)
    {
        return entityId switch
        {
            (int)EntityId.Tower => (3, 3),
            (int)EntityId.King => (4, 4),
            (int)EntityId.Larry => (1, 1),
            _ => throw new ArgumentOutOfRangeException(
                nameof(entityId),
                entityId,
                "Unknown entity id"
            ),
        };
    }
}
