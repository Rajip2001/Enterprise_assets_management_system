using EAMS.Application.Features.Users.DTOs;
using MediatR;

namespace EAMS.Application.Features.Users.Commands.ChangeUserRole;

public record ChangeUserRoleCommand(
    Guid UserId,
    Guid RoleId
) : IRequest<UserResponse>;