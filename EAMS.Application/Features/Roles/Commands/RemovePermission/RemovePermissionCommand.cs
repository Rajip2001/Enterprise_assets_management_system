using MediatR;

namespace EAMS.Application.Features.Roles.Commands.RemovePermission;

public record RemovePermissionCommand(
    Guid RoleId,
    Guid PermissionId
) : IRequest;   