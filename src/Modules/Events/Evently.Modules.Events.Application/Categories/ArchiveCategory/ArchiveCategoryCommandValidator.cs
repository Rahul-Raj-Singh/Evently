using FluentValidation;

namespace Evently.Modules.Events.Application.Categories.ArchiveCategory;

public sealed class ArchiveCategoryCommandValidator : AbstractValidator<ArchiveCategoryCommand>
{
    public ArchiveCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryId).NotEmpty();
    }
}
