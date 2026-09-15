using EAMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Users.Commands.DeactivateUser;

public class DeactivateUserCommandHandler
    : IRequestHandler<DeactivateUserCommand>
{
    private readonly IApplicationDbContext _context;

    public DeactivateUserCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeactivateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException(
                $"User with ID '{request.Id}' was not found.");
        }

        if (!user.IsActive)
        {
            throw new InvalidOperationException(
                "User is already inactive.");
        }

        user.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
