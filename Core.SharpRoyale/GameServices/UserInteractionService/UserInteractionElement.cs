namespace Core.SharpRoyale.GameServices.UserInteractionService;

public record UserInteractionElement(Match match, Player player, UserInteractionOption action, dynamic values);