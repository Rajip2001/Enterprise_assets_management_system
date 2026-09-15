using FluentValidation;

namespace EAMS.Application.Features.Roles.Commands.RemovePermission;

public class RemovePermissionCommandValidator
    : AbstractValidator<RemovePermissionCommand>
{
    public RemovePermissionCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Role ID is required.");

        RuleFor(x => x.PermissionId)
            .NotEmpty()
            .WithMessage("Permission ID is required.");
    }
}
