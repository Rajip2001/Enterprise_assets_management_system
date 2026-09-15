using EAMS.Application.Features.Roles.DTOs;
using MediatR;

namespace EAMS.Application.Features.Roles.Queries.GetRolePermissions;

public record GetRolePermissionsQuery(
    Guid RoleId
) : IRequest<List<RolePermissionResponse>>;
