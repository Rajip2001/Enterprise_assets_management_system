using FluentValidation;

namespace EAMS.Application.Features.Roles.Commands.AssignPermission;

public class AssignPermissionCommandValidator
    : AbstractValidator<AssignPermissionCommand>
{
    public AssignPermissionCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Role ID is required.");

        RuleFor(x => x.PermissionId)
            .NotEmpty()
            .WithMessage("Permission ID is required.");
    }
}
