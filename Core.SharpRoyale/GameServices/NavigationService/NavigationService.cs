using Core.SharpRoyale.GameServices.ActionListService;

namespace Core.SharpRoyale.GameServices.NavigationService;

public static class NavigationService
{
    private static readonly Position[] _bridgePositions =
    {
        new Position(3.5, 14.5),
        new Position(12, 15),
    };

    // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18
    public static Position GetNextNavigation(Entity entity, Match match)
    {
        Position navTarget = _bridgePositions[0];
        double closestDistanceSquared = double.MaxValue;

        foreach (Position bridgePosition in _bridgePositions)
        {
            double bridgeDx = bridgePosition.X - entity.Pos.X;
            double bridgeDy = bridgePosition.Y - entity.Pos.Y;
            double distanceSquared = bridgeDx * bridgeDx + bridgeDy * bridgeDy;

            if (distanceSquared < closestDistanceSquared)
            {
                closestDistanceSquared = distanceSquared;
                navTarget = bridgePosition;
            }
        }

        double dx = navTarget.X - entity.Pos.X;
        double dy = navTarget.Y - entity.Pos.Y;

        double distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance <= 1.0)
        {
            return navTarget;
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
