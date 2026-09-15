using EAMS.Application.Features.Permissions.DTOs;
using MediatR;

namespace EAMS.Application.Features.Permissions.Queries.GetPermissionById;

public record GetPermissionByIdQuery(
    Guid Id
) : IRequest<PermissionResponse>;