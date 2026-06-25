namespace Core.SharpRoyale;
public abstract class Entity
{
    public abstract int Id { get; }
    public abstract int Owner { get; set; }

    public Entity? FollowTarget { get; set; }

    public abstract (ushort x, ushort y) Pos { get; set; }

    public abstract int Width { get; }
    public abstract int Height { get; }
    public abstract int ElixirCost { get; }
    public abstract bool RestrictedDeployment { get; }

    public abstract Entity ProcessDeployment(ushort x, ushort y);

    public abstract void ProcessDamage();

    public abstract void ProcessDebuff();

    public abstract void Tick();
}
