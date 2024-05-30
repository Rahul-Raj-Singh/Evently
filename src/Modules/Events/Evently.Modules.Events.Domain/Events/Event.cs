using System.Runtime.InteropServices;
using Evently.Modules.Events.Domain.Abstractions;
using Evently.Modules.Events.Domain.Categories;

namespace Evently.Modules.Events.Domain.Events;

public class Event : Entity
{
    public Guid Id { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Location { get; private set; }
    public DateTime StartsAtUtc { get; private set; }
    public DateTime? EndsAtUtc { get; private set; }
    public EventStatus Status { get; private set; }

    public static Result<Event> Create(
        Category category,
        string title,
        string description,
        string location,
        DateTime startsAtUtc,
        DateTime? endsAtUtc)
    {
        if (endsAtUtc.HasValue && endsAtUtc <= startsAtUtc)
        {
            return Result.Failure<Event>(EventErrors.EndDatePrecedesStartDate);
        }

        var @event = new Event
        {
            Id = Guid.NewGuid(),
            CategoryId = category.Id,
            Title = title,
            Description = description,
            Location = location,
            StartsAtUtc = startsAtUtc,
            EndsAtUtc = endsAtUtc,
            Status = EventStatus.Draft
        };

        @event.Raise(new EventCreatedDomainEvent(@event.Id));

        return Result.Success(@event);
    }

    public Result Publish()
    {
        if (Status != EventStatus.Draft)
        {
            return Result.Failure<Result>(EventErrors.NotDraft);
        }
        
        Status = EventStatus.Published;

        Raise(new EventPublishedDomainEvent(Id));

        return Result.Success();
    }

    public Result Reschedule(DateTime startsAtUtc, DateTime? endsAtUtc)
    {
        if (StartsAtUtc == startsAtUtc && EndsAtUtc == endsAtUtc)
        {
            return Result.Success();
        }

        StartsAtUtc = startsAtUtc;
        EndsAtUtc = endsAtUtc;

        Raise(new EventRescheduledDomainEvent(Id, StartsAtUtc, EndsAtUtc));

        return Result.Success();
    }

    public Result Cancel(DateTime utcNow)
    {
        if (Status == EventStatus.Cancelled)
        {
            return Result.Failure(EventErrors.AlreadyCanceled);
        }

        if (StartsAtUtc < utcNow)
        {
            return Result.Failure(EventErrors.AlreadyStarted);
        }

        Status = EventStatus.Cancelled;

        Raise(new EventCancelledDomainEvent(Id));

        return Result.Success();
    }

}
