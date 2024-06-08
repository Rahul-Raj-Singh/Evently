namespace Evently.Common.Domain;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccuredOnUtc = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime occuredOnUtc)
    {
        Id = id;
        OccuredOnUtc = occuredOnUtc;
    }
    protected DomainEvent(Guid id)
    {
        Id = id;
        OccuredOnUtc = DateTime.UtcNow;
    }

    public Guid Id {get; init;}
    public DateTime OccuredOnUtc {get; init;}
}
