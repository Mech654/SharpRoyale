using Core.SharpRoyale.GameServices.ActionListService;

namespace Core.SharpRoyale.GameServices.NavigationService;

public static class NavigationService
{
    private const double BridgeTargetYOffset = -0.5;
    private const double BridgeExitOffset = 0;

    private static readonly Position[] _bridgePositions =
    {
        new Position(3.5, 16),
        new Position(14.5, 16),
    };

    // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18
    public static Position GetNextNavigation(Entity entity, Match match)
    {
        Position navTarget = GetNavigationTarget(entity, match);

        double dx = navTarget.X - entity.Pos.X;
        double dy = navTarget.Y - entity.Pos.Y;

        double distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance <= 1.0)
        {
            return navTarget;
        }

        double stepX = dx / distance;
        double stepY = dy / distance;

        return new Position(entity.Pos.X + stepX, entity.Pos.Y + stepY);
    }

    private static Position GetNavigationTarget(Entity entity, Match match)
    {
        double closestDistanceSquared = double.MaxValue;

        // TODO: Other factors such as enemy presence or other constructions will come above
        Position navTarget = _bridgePositions[0];
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

        if (IsOnBridge(entity, navTarget))
        {
            return GetExitBridgeTarget(entity, navTarget);
        }

        if (IsPastBridges(entity))
        {
            return entity.Pos; // Just stop for not TODO:
        }

        navTarget = GetEnterBridgeTarget(entity, navTarget);

        return navTarget;
    }

    private static bool IsOnBridge(Entity entity, Position bridgePosition)
    {
        return entity.IsMirrored
            ? entity.Pos.Y >= bridgePosition.Y - 1
            : entity.Pos.Y <= bridgePosition.Y + 1;
    }

    private static bool IsPastBridges(Entity entity)
    {
        return entity.IsMirrored
            ? entity.Pos.Y >= _bridgePositions[0].Y - 1
            : entity.Pos.Y <= _bridgePositions[0].Y + 1;
    }

    private static Position GetExitBridgeTarget(Entity entity, Position bridgePosition)
    {
        return entity.IsMirrored
            ? bridgePosition with
            {
                Y = bridgePosition.Y + 1,
            }
            : bridgePosition with
            {
                Y = bridgePosition.Y - 1,
            };
    }

    private static Position GetEnterBridgeTarget(Entity entity, Position bridgePosition)
    {
        return entity.IsMirrored
            ? bridgePosition with
            {
                Y = bridgePosition.Y - 1,
            }
            : bridgePosition with
            {
                Y = bridgePosition.Y + 1,
            };
    }

    public static Entity MoveEntity(Entity entity, Position newPos)
    {
        // keeping it simple for now
        entity.Pos = newPos;
        return entity;
    }
}
