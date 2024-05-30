namespace Evently.Modules.Events.Application;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}