using EAMS.Application.Features.Roles.DTOs;
using MediatR;

namespace EAMS.Application.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand(
    string Name,
    string? Description
) : IRequest<RoleResponse>;
