using Evently.Modules.Events.Application;

namespace Evently.Modules.Events.Infrastructure.Clock;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
