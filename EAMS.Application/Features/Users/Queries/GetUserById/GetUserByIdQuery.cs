using EAMS.Application.Features.Users.DTOs;
using MediatR;

namespace EAMS.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserResponse>;