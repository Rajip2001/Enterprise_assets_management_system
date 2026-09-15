using EAMS.Application.Features.Users.DTOs;
using MediatR;

namespace EAMS.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    string? PhoneNumber,
    Guid RoleId
) : IRequest<UserResponse>;
