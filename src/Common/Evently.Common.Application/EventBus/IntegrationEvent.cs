
namespace Evently.Common.Application.EventBus;

public class IntegrationEvent : IIntegrationEvent
{
    protected IntegrationEvent(Guid id, DateTime occuredOnUtc)
    {
        Id = id;
        OccuredOnUtc = occuredOnUtc;
    }

    public Guid Id {get; init;}

    public DateTime OccuredOnUtc {get; init;}
}
