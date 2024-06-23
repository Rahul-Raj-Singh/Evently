using Evently.Common.Domain;

namespace Evently.Common.Application.Exceptions;

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