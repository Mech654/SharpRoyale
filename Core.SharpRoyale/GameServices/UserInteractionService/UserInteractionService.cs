using Core.SharpRoyale.GameServices.SpawnService;
namespace Core.SharpRoyale.GameServices.UserInteractionService;

public static class UserInteractionService
{
    public static bool ApplyUserInteraction(UserInteractionElement userInteractionElement)
    {
        switch (userInteractionElement.action)
        {
            case UserInteractionOption.Spawn:
                SpawnService.SpawnService.SpawnSingularEntity(userInteractionElement);
                return true;
        }

        return false;
    }
}
