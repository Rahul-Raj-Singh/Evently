namespace Evently.Common.Domain;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity() {}

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() 
    { 
        return _domainEvents.ToList() ; 
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
