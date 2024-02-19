using BookLibrary.Application.Commands.Opinions.Create;
using FluentValidation;

namespace BookLibrary.Infrastructure.Commands.Opinions.Create;

public class CreateOpinionValidator : AbstractValidator<CreateOpinionCommand>
{
    public CreateOpinionValidator()
    {
        RuleFor(v => v.BookId)
            .NotNull();

        RuleFor(v => v.Reaction)
            .NotEmpty()
            .IsInEnum();
    }
}