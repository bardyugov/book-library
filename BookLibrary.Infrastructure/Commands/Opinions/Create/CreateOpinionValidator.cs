using BookLibrary.Application.Commands.Opinions.Create;
using BookLibrary.Domain.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace BookLibrary.Infrastructure.Commands.Opinions.Create;

public class CreateOpinionValidator : AbstractValidator<CreateOpinionCommand>
{
    public CreateOpinionValidator()
    {
        RuleFor(v => v.BookId)
            .NotNull();

        RuleFor(v => v.Reaction)
            .IsInEnum();
    }
}