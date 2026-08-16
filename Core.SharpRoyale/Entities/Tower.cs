using Core.SharpRoyale.GameServices.ActionListService;
using Core.SharpRoyale.GameServices.NavigationService;

namespace Core.SharpRoyale.Entities;

public class Tower(int owner, Match match) : Entity(owner, match.GetNextEntityId())
{
    public override int EntityId { get; } = 1;
    public override int Width { get; } = 3;
    public override int Height { get; } = 3;
    public override int ElixirCost { get; } = 0;
    public override bool RestrictedDeployment { get; } = true;
    public override int Speed { get; } = 0;
    public override bool IsConstruction { get; } = true;
    public override float HitboxRadius { get; } = 0f;
    public override ushort AttackDistance { get; } = 10;

    public override Entity ProcessDeployment(ushort x, ushort y)
    {
        Pos = new Position(x, y);
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

    public override void Tick() { }

    private void ApplyAction(ActionListOption option, object values)
    {
        //ActionListService.AppendActionList(option, values, this, match );
    }
}
