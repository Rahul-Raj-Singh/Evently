using FluentValidation;

namespace Evently.Modules.Events.Application.Events.PublishEvent;

public sealed class PublishEventCommandValidator : AbstractValidator<PublishEventCommand>
{
    public PublishEventCommandValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();
    }
}
