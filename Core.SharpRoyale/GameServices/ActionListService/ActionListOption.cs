namespace Core.SharpRoyale.GameServices.ActionListService;

public enum ActionListOption
{
    Spawn,
    Move,
    Attack,
    Die,
    Exit,
}

public abstract record ActionListValue();

public readonly record struct Position(int X, int Y);

public record ActionListValueSpawn(Position Position, EntityId EntityId) : ActionListValue;
