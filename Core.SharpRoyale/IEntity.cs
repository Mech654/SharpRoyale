namespace Core.SharpRoyale;

public interface IEntity
{
    int Owner { get; set; }
    
    (ushort x, ushort y) Pos { get; set; }
    int Width { get; }
    int Height { get; }
    IEntity ProcessDeployment(ushort x, ushort y);
    void ProcessDamage();
    void ProcessDebuff();
    void Tick();
}