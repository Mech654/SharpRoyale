namespace Core.SharpRoyale;
public abstract class Entity
{
    public int Owner { get; set; }

    public (ushort x, ushort y) Pos { get; set; }

    public abstract int Width { get; }
    public abstract int Height { get; }

    public abstract Entity ProcessDeployment(ushort x, ushort y);

    public abstract void ProcessDamage();

    public abstract void ProcessDebuff();

    public abstract void Tick();
}