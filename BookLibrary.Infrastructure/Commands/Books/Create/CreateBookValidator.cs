using BookLibrary.Application.Commands.Books.Create;
using FluentValidation;

namespace BookLibrary.Infrastructure.Commands.Books.Create;

public class CreateBookValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookValidator()
    {
        
        RuleFor(v => v.Name)
            .NotNull()
            .Length(3, 20);

        RuleFor(v => v.Description)
            .NotNull()
            .Length(6, 20);

        RuleFor(v => v.File)
            .NotNull();

    }
}