namespace Core.SharpRoyale.Entities;

public class Tower(int owner) : IEntity
{
    public int Owner { get; set; } = owner;
    
    public (ushort x, ushort y) Pos { get; set; }
    public int Width { get; } = 2;
    public int Height { get; } = 2;
    

    public IEntity ProcessDeployment(ushort x, ushort y)
    {
        Pos = (x, y);
        return this;
    }
    public void ProcessDamage()
    {
        throw new NotImplementedException();
    }
    public void ProcessDebuff()
    {
        throw new NotImplementedException();
    }
    public void Act()
    {
        throw new NotImplementedException();
    }
}