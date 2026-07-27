using Core.SharpRoyale.GameServices.ActionListService;

namespace Core.SharpRoyale.GameServices.NavigationService;

public static class NavigationService
{
    private static readonly Position _leftBridgePosition = new Position(3, 16);
    private static readonly Position _rightBridgePosition = new Position(15, 16);

    // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18
    public static Position GetNextNavigation(Entity entity, Match match)
    {
        Position navTarget;
        // Aggro/enemy following logic
        // Not implemented yet
        // Standard "Towards enemy tower" nav
        if (entity.Pos.X >= 9)
        {
            navTarget = _leftBridgePosition;
        }
        else
        {
            navTarget = _rightBridgePosition;
        }

        double dx = navTarget.X - entity.Pos.X;
        double dy = navTarget.Y - entity.Pos.Y;

        double distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance == 0)
        {
            return entity.Pos;
        }

        double stepX = dx / distance;
        double stepY = dy / distance;

        Console.WriteLine(
            entity.Id
                + "called. New position is "
                + " X: "
                + entity.Pos.X
                + stepX
                + " Y: "
                + entity.Pos.Y
                + stepY
        );
        return new Position(entity.Pos.X + stepX, entity.Pos.Y + stepY);
    }

    public static Entity MoveEntity(Entity entity, Position newPos)
    {
        // keeping it simple for now
        entity.Pos = newPos;
        return entity;
    }
}
