using BookLibrary.Application.Commands.Authors.Create;
using FluentValidation;

namespace BookLibrary.Infrastructure.Commands.Authors.Create;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .Length(3, 20);

        RuleFor(v => v.Surname)
            .NotEmpty()
            .Length(3, 20);

        RuleFor(v => v.Password)
            .NotEmpty()
            .Length(3, 20);

        RuleFor(v => v.Patronymic)
            .NotEmpty()
            .Length(3, 20);

        RuleFor(v => v.Email)
            .NotEmpty()
            .Length(3, 20)
            .EmailAddress();

        RuleFor(v => v.Age)
            .NotEmpty();
    }
}