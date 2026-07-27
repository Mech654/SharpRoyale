using Core.SharpRoyale.GameServices.ActionListService;
using Core.SharpRoyale.GameServices.NavigationService;

namespace Core.SharpRoyale.Entities;

public class Larry(int owner, Match match) : Entity(owner, match.GetNextEntityId())
{
    public override int EntityId { get; } = 3;
    public override int Width { get; } = 1;
    public override int Height { get; } = 1;
    public override int ElixirCost { get; } = 1;
    public override bool RestrictedDeployment { get; } = true;
    public override int Speed { get; } = 1;

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

    public override void Tick()
    {
        Console.WriteLine($"LARRY: {EntityId}");
        MoveAccumulator += Speed * TickRate;
        Console.WriteLine(MoveAccumulator);

        while (MoveAccumulator >= 1.0)
        {
            Position nextPos = NavigationService.GetNextNavigation(this, match);
            ActionListService.AppendActionListMove(new ActionListValueMove(nextPos, this), match);
            MoveAccumulator -= 1.0;
        }
    }
}
