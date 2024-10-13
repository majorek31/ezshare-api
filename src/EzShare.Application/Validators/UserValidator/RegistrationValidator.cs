using EzShare.Application.Features.User.Commands.Register;
using FluentValidation;

namespace EzShare.Application.Validators.UserValidator;

public class RegistrationValidator : AbstractValidator<RegisterCommand>
{
    public RegistrationValidator()
    {
        RuleFor(x => x.Dto.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Dto.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    }
}