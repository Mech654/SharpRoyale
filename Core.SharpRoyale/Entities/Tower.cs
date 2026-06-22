using Core.SharpRoyale.GameServices.ActionListService;

namespace Core.SharpRoyale.Entities;

public class Tower(int owner, Match match) : IEntity
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
    public void Tick()
    {
        // Dummy Section
        var destination = NavigationService.GetNextNavigation(this, match);
        ApplyAction(ActionListOption.Move, destination);

    }

    private void ApplyAction(ActionListOption option, object values)
    {
        ActionListService.AppendActionList(option, values, this, match );
    }
}