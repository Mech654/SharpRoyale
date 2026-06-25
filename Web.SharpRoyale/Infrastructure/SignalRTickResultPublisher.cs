using Engine.SharpRoyale;
using Microsoft.AspNetCore.SignalR;
using Web.SharpRoyale.Hubs;

namespace Web.SharpRoyale.Infrastructure;

public class SignalRTickResultPublisher(IHubContext<MatchHub> hubContext) : ITickResultPublisher
{
    public Task PublishTickResultAsync(TickResultDto tickResult, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(tickResult);

        return hubContext.Clients
            .Group($"match:{tickResult.MatchId}")
            .SendAsync("TickResult", tickResult, cancellationToken);
    }
}
