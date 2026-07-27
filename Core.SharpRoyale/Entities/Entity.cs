using Core.SharpRoyale.GameServices.ActionListService;

namespace Core.SharpRoyale;

public abstract class Entity(int Owner, int id)
{
    public int Id { get; } = id;
    public abstract int EntityId { get; }
    public int Owner { get; } = Owner;

    public Position Pos { get; set; }
    public abstract int Speed { get; }
    public double MoveAccumulator { get; set; }
    public double TickRate = 1.0 / 60;

    public abstract int Width { get; }
    public abstract int Height { get; }
    public abstract int ElixirCost { get; }
    public abstract bool RestrictedDeployment { get; }

    public abstract Entity ProcessDeployment(ushort x, ushort y);

    public abstract void ProcessDamage();

    public abstract void ProcessDebuff();

    public abstract void Tick();
}

public enum EntityId
{
    Tower = 1,
    King = 2,
    Larry = 3,
}
