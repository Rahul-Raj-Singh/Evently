namespace Evently.Common.Domain;
public interface IDomainEvent
{
    Guid Id { get; }
    DateTime OccuredOnUtc { get; }
}