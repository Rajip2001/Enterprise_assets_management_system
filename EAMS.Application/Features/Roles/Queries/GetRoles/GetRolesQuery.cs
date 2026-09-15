using EAMS.Application.Features.Roles.DTOs;
using MediatR;

namespace EAMS.Application.Features.Roles.Queries.GetRoles;

public record GetRolesQuery : IRequest<List<RoleResponse>>;
