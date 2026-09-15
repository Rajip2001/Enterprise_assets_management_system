using FluentValidation;

namespace EAMS.Application.Features.Users.Commands.ChangeUserRole;

public class ChangeUserRoleCommandValidator
    : AbstractValidator<ChangeUserRoleCommand>
{
    public ChangeUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Role ID is required.");
    }
}