using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Application.Abstractions.Exceptions;

public class EventlyException : Exception
{
    public EventlyException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application Exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }
    public string RequestName { get; }
    public Error? Error { get; }
}