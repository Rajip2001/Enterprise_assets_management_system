using MediatR;

namespace EAMS.Application.Features.Users.Commands.DeactivateUser;

public record DeactivateUserCommand(Guid Id) : IRequest;
