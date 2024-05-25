using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Infrastructure.Database;

namespace Evently.Modules.Events.Infrastructure.Events;

public sealed class EventRepository(EventsDbContext dbContext) : IEventRepository
{
    public void Insert(Event @event)
    {
        dbContext.Events.Add(@event);
    }
}
