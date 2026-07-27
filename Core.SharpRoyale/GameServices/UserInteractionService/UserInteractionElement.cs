namespace Core.SharpRoyale.GameServices.UserInteractionService;

public record UserInteractionElement(
    Match match,
    int playerid,
    UserInteractionOption action,
    dynamic values
);
