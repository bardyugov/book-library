using BookLibrary.Application.Commands.Comments.Create;
using FluentValidation;

namespace BookLibrary.Infrastructure.Commands.Comments.Create;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(v => v.Content)
            .NotEmpty()
            .Length(6, 20);

        RuleFor(v => v.NameBook)
            .NotEmpty()
            .Length(3, 20);
    }
}