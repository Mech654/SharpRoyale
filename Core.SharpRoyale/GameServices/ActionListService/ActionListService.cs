using Core.SharpRoyale;

namespace Core.SharpRoyale.GameServices.ActionListService;

public record ActionElement(
    Entity Entity,
    ActionListOption Option,
    ActionListValue Values,
    DateTime Time
);

public static class ActionListService
{
    public static void AppendActionListSpawn(
        ActionListValueSpawn values,
        Entity entity,
        Match match
    )
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(match);

        match.ActionList.Add(
            new ActionElement(entity, ActionListOption.Spawn, values, DateTime.UtcNow)
        );
        SortActionList(match);
    }

    public static void ApplyActionList(Match match)
    {
        ArgumentNullException.ThrowIfNull(match);

        var actionList = GetResolvedActionList(match);

        foreach (var actionElement in actionList)
        {
            switch (actionElement.Option)
            {
                case ActionListOption.Spawn:
                    ApplySpawnAction(actionElement);
                    break;
                case ActionListOption.Move:
                    ApplyMoveAction(actionElement);
                    break;
                case ActionListOption.Attack:
                    ApplyAttackAction(actionElement);
                    break;
                case ActionListOption.Die:
                    ApplyDieAction(actionElement);
                    break;
                case ActionListOption.Exit:
                    ApplyExitAction(actionElement);
                    break;
            }
        }

        match.ActionList.Clear();
    }

    public static IReadOnlyList<ActionElement> GetResolvedActionList(Match match)
    {
        ArgumentNullException.ThrowIfNull(match);

        return match.ActionList.OrderBy(GetActionPhase).ToList();
    }

    private static void SortActionList(Match match)
    {
        // attack-style actions are processed before movement-style actions.
        var ordered = match.ActionList.OrderBy(GetActionPhase).ToList();

        match.ActionList.Clear();
        match.ActionList.AddRange(ordered);
    }

    private static int GetActionPhase(ActionElement actionElement)
    {
        return actionElement.Option switch
        {
            ActionListOption.Spawn => 0,
            ActionListOption.Attack => 1,
            ActionListOption.Move => 2,
            ActionListOption.Die => 3,
            ActionListOption.Exit => 4,
            _ => 2,
        };
    }

    private static void ApplySpawnAction(ActionElement actionElement)
    {
        // TODO: apply spawn logic.
    }

    private static void ApplyMoveAction(ActionElement actionElement)
    {
        // TODO: apply move logic.
    }

    private static void ApplyAttackAction(ActionElement actionElement)
    {
        // TODO: apply attack logic.
    }

    private static void ApplyDieAction(ActionElement actionElement)
    {
        // TODO: apply die logic.
    }

    private static void ApplyExitAction(ActionElement actionElement)
    {
        // TODO: apply exit logic.
    }
}
