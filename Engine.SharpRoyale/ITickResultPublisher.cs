namespace Engine.SharpRoyale;

public interface ITickResultPublisher
{
    Task PublishTickResultAsync(TickResultDto tickResult, CancellationToken cancellationToken = default);
}
