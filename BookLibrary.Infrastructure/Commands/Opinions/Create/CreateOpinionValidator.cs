using BookLibrary.Application.Commands.Opinions.Create;
using BookLibrary.Domain.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace BookLibrary.Infrastructure.Commands.Opinions.Create;

public class UpdateIndicatorEnumValidator<T> : PropertyValidator
{
    public UpdateIndicatorEnumValidator() : base("Invalid update indicator") {}

    protected override bool IsValid(PropertyValidatorContext context)
    {
        Reaction enumVal = (Reaction)Enum.Parse(typeof(Reaction), context.PropertyValue.ToString());

        if (!Enum.IsDefined(typeof(UpdateIndicator), enumVal))
            return false;

        return true;
    }
}

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