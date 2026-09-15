using MediatR;

namespace EAMS.Application.Features.Roles.Commands.AssignPermission;

public record AssignPermissionCommand(
    Guid RoleId,
    Guid PermissionId
) : IRequest;
