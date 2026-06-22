namespace Core.SharpRoyale.GameServices.ActionListService;

public record ActionElement(IEntity Entity, ActionListOption Option, object Values, DateTime Time);

public static class ActionListService
{
    public static void AppendActionList(ActionListOption option, object values, IEntity entity, Match match)
    {
        var time = DateTime.Now;
        match.ActionList.Add(new ActionElement(entity, option, values, time));
    }
}