using EAMS.Application.Features.Users.DTOs;
using MediatR;

namespace EAMS.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string? PhoneNumber,
    Guid RoleId,
    bool IsActive
) : IRequest<UserResponse>;
