using EAMS.Application.Features.Roles.DTOs;
using MediatR;

namespace EAMS.Application.Features.Roles.Commands.UpdateRole;

public record UpdateRoleCommand(
    Guid Id,
    string Name,
    string? Description
) : IRequest<RoleResponse>;
