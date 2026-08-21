using Core.SharpRoyale.GameServices.ActionListService;

namespace Core.SharpRoyale.GameServices.NavigationService;

public static class NavigationService
{
    private static readonly Position[] BridgePositions =
    {
        new Position(3.5, 16),
        new Position(14.5, 16),
    };

    // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18
    public static Position GetNextNavigation(Entity entity, Match match, double deltaTime)
    {
        Position navTarget = GetNavigationTarget(entity, match);

        double dx = navTarget.X - entity.Pos.X;
        double dy = navTarget.Y - entity.Pos.Y;

        double distance = Math.Sqrt(dx * dx + dy * dy);

        double stepSize = entity.Speed * deltaTime;

        if (distance <= stepSize || distance == 0)
        {
            return navTarget;
        }

        double stepX = dx / distance;
        double stepY = dy / distance;

        return new Position(entity.Pos.X + stepX * stepSize, entity.Pos.Y + stepY * stepSize);
    }

    private static Entity GetClosestEnemyTower(Entity entity, Match match)
    {
        Console.WriteLine("GetClosestTowerTriggered");
        double closestDistanceSquared = double.MaxValue;

        Entity navTarget = entity;
        List<Entity> candidates = match
            .Map.Entities.Where(xEntity =>
                xEntity.EntityId is 1 or 2 && xEntity.Owner != entity.Owner
            )
            .ToList();

        foreach (Entity tower in candidates)
        {
            double towerDx = tower.Pos.X - entity.Pos.X;
            double towerDy = tower.Pos.Y - entity.Pos.Y;
            double distanceSquared = towerDx * towerDx + towerDy * towerDy;

            if (distanceSquared < closestDistanceSquared)
            {
                closestDistanceSquared = distanceSquared;
                navTarget = tower;
            }
        }

        return navTarget;
    }

    private static Position GetNavigationTarget(Entity entity, Match match)
    {
        double closestDistanceSquared = double.MaxValue;

        // TODO: Other factors such as enemy presence or other constructions will come above
        Entity? closestEntity = null;
        double closestEntityDistance = Double.MaxValue;

        foreach (
            Entity target in match.Map.Entities.Where(xEntity => xEntity.Owner != entity.Owner)
        )
        {
            if (IsWithinAggroRange(entity, target))
            {
                if (closestEntity is null)
                {
                    closestEntity = target;
                    closestEntityDistance = GetDistanceToHitbox(entity, target);
                }
                else if (GetDistanceToHitbox(entity, target) < closestEntityDistance)
                {
                    closestEntity = target;
                    closestEntityDistance = GetDistanceToHitbox(entity, target);
                }
            }
        }

        Position closestBridge = BridgePositions[0];
        foreach (Position bridgePosition in BridgePositions)
        {
            double bridgeDx = bridgePosition.X - entity.Pos.X;
            double bridgeDy = bridgePosition.Y - entity.Pos.Y;
            double distanceSquared = bridgeDx * bridgeDx + bridgeDy * bridgeDy;

            if (distanceSquared < closestDistanceSquared)
            {
                closestDistanceSquared = distanceSquared;
                closestBridge = bridgePosition;
            }
        }

        if (!IsPastBridges(entity))
        {
            if (closestEntity is not null && IsPastBridges(closestEntity))
                return GetCollisionPoint(entity, closestEntity);
            return GetEnterBridgeTarget(entity, closestBridge);
        }

        if (IsPastBridges(entity))
        {
            if (closestEntity is not null)
            {
                if (!IsPastBridges(closestEntity))
                    return GetCollisionPoint(entity, closestEntity);
                if (IsPastBridges(closestEntity))
                {
                    if (entity.IsMirrored)
                    {
                        Console.WriteLine("mirrored");
                        if (entity.Pos.Y <= BridgePositions[0].Y + 2)
                        {
                            double bridgeDistance = GetDistanceToPosition(
                                entity.Pos,
                                GetEnterBridgeTarget(entity, closestBridge)
                            );
                            double enemyDistance = GetDistanceToPosition(
                                entity.Pos,
                                closestEntity.Pos
                            );

                            if (enemyDistance <= bridgeDistance + 2)
                                return GetCollisionPoint(entity, closestEntity);
                            return GetEnterBridgeTarget(entity, closestBridge);
                        }
                    }

                    if (!entity.IsMirrored)
                    {
                        if (entity.Pos.Y >= BridgePositions[0].Y - 2)
                        {
                            Console.WriteLine("not mirrored");
                            double bridgeDistance = GetDistanceToPosition(
                                entity.Pos,
                                GetEnterBridgeTarget(entity, closestBridge)
                            );
                            double enemyDistance = GetDistanceToPosition(
                                entity.Pos,
                                closestEntity.Pos
                            );

                            if (enemyDistance <= bridgeDistance + 2)
                                return GetCollisionPoint(entity, closestEntity);
                            return GetEnterBridgeTarget(entity, closestBridge);
                        }
                    }

                    return GetExitBridgeTarget(entity, closestBridge);
                }
                // need to return closest bridge position
            }
            return GetCollisionPoint(entity, GetClosestEnemyTower(entity, match));
        }

        return closestBridge;
    }

    private static bool IsPastBridges(Entity entity)
    {
        return entity.IsMirrored
            ? entity.Pos.Y >= BridgePositions[0].Y - 2
            : entity.Pos.Y <= BridgePositions[0].Y + 2;
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

    public static Position GetCollisionPoint(Entity entity, Entity target)
    {
        double dx = target.Pos.X - entity.Pos.X;
        double dy = target.Pos.Y - entity.Pos.Y;

        double distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance == 0)
            return entity.Pos;

        double directionX = dx / distance;
        double directionY = dy / distance;

        double collisionDistance;

        if (target.IsConstruction)
        {
            // Distance from target center to its edge in the direction of the entity
            double halfWidth = target.Width / 2.0;
            double halfHeight = target.Height / 2.0;

            double distanceX = halfWidth / Math.Abs(directionX);
            double distanceY = halfHeight / Math.Abs(directionY);

            double targetEdgeDistance = Math.Min(distanceX, distanceY);

            // Entity is circular, so stop when its hitbox touches the construction.
            collisionDistance = targetEdgeDistance - entity.HitboxRadius;
        }
        else
        {
            // Both entities have circular hitboxes.
            collisionDistance = distance - entity.HitboxRadius - target.HitboxRadius;
        }

        collisionDistance = Math.Max(collisionDistance, 0);

        return new Position(
            entity.Pos.X + directionX * collisionDistance,
            entity.Pos.Y + directionY * collisionDistance
        );
    }

    private static double GetDistanceToHitbox(Entity a, Entity b)
    {
        double dx = a.Pos.X - b.Pos.X;
        double dy = a.Pos.Y - b.Pos.Y;

        double centerDistance = Math.Sqrt(dx * dx + dy * dy);

        return Math.Max(centerDistance - a.HitboxRadius - b.HitboxRadius, 0);
    }

    private static double GetDistanceToConstruction(Entity a, Entity b)
    {
        double dx = Math.Abs(a.Pos.X - b.Pos.X) - b.Width / 2.0;
        double dy = Math.Abs(a.Pos.Y - b.Pos.Y) - b.Height / 2.0;

        dx = Math.Max(dx, 0);
        dy = Math.Max(dy, 0);

        return Math.Sqrt(dx * dx + dy * dy);
    }

    private static double GetDistanceToPosition(Position a, Position b)
    {
        double dx = a.X - b.X;
        double dy = a.Y - b.Y;

        double centerDistance = Math.Sqrt(dx * dx + dy * dy);

        return Math.Max(centerDistance, 0);
    }

    private static bool IsWithinAggroRange(Entity entity, Entity target)
    {
        return (GetDistanceToHitbox(entity, target) <= entity.AggroRange);
    }
}
