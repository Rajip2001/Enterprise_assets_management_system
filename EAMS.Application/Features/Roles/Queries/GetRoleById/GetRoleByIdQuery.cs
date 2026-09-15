using EAMS.Application.Features.Roles.DTOs;
using MediatR;

namespace EAMS.Application.Features.Roles.Queries.GetRoleById;

public record GetRoleByIdQuery(Guid Id) : IRequest<RoleResponse>;