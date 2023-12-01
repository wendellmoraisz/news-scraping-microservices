namespace EventBus.Messages.Events;

public class BaseIntegrationEvent
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public BaseIntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public BaseIntegrationEvent(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }
}