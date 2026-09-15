using EAMS.Application.Features.Users.DTOs;
using MediatR;

namespace EAMS.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<List<UserResponse>>;
