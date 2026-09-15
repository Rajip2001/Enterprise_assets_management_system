using EAMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler
    : IRequestHandler<DeleteRoleCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRoleCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (role is null)
        {
            throw new KeyNotFoundException(
                $"Role with ID '{request.Id}' was not found.");
        }

        var hasUsers = await _context.Users
            .AnyAsync(
                x => x.RoleId == role.Id,
                cancellationToken);

        if (hasUsers)
        {
            throw new InvalidOperationException(
                $"Role '{role.Name}' cannot be deleted because users are assigned to it.");
        }

        _context.Roles.Remove(role);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
