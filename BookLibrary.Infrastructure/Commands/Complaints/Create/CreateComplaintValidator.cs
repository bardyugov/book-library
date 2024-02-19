using BookLibrary.Application.Commands.Complaints.Create;
using FluentValidation;

namespace BookLibrary.Infrastructure.Commands.Complaints.Create;

public class CreateComplaintValidator : AbstractValidator<CreateComplaintCommand>
{
    public CreateComplaintValidator()
    {
        RuleFor(v => v.Content)
            .NotEmpty()
            .Length(6, 20);

        RuleFor(v => v.BookId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}