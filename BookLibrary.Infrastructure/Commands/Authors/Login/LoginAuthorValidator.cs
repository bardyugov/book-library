using BookLibrary.Application.Commands.Authors.Login;
using FluentValidation;

namespace BookLibrary.Infrastructure.Commands.Authors.Login;

public class LoginAuthorValidator : AbstractValidator<LoginAuthorCommand>
{
    public LoginAuthorValidator()
    {
        RuleFor(v => v.Password)
            .NotEmpty()
            .Length(3, 20);

        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();
    }
}