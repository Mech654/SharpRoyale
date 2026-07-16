using Core.SharpRoyale.GameServices.ActionListService;
using Core.SharpRoyale.GameServices.NavigationService;

namespace Core.SharpRoyale.Entities;

public class Tower(int owner, Match match) : Entity
{
    public override int Id { get; } = 1;
    public override int Width { get; } = 2;
    public override int Height { get; } = 2;
    public override int ElixirCost { get; } = 0;
    public override bool RestrictedDeployment { get; } = true;

    public override int Owner { get; set; } = owner;
    public override (ushort x, ushort y) Pos { get; set; }

    public override Entity ProcessDeployment(ushort x, ushort y)
    {
        Pos = (x, y);
        return this;
    }

    public override void ProcessDamage()
    {
        throw new NotImplementedException();
    }

    public override void ProcessDebuff()
    {
        throw new NotImplementedException();
    }

    public override void Tick()
    {
        // Dummy Section
        var destination = NavigationService.GetNextNavigation(this, match);
        ApplyAction(ActionListOption.Move, destination);
    }

    private void ApplyAction(ActionListOption option, object values)
    {
        //ActionListService.AppendActionList(option, values, this, match );
    }
}
