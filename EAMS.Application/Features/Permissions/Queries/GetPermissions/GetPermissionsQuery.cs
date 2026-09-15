using EAMS.Application.Features.Permissions.DTOs;
using MediatR;

namespace EAMS.Application.Features.Permissions.Queries.GetPermissions;

public record GetPermissionsQuery
    : IRequest<List<PermissionResponse>>;
