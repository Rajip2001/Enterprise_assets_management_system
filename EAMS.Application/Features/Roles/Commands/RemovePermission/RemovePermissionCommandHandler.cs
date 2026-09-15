using EAMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Roles.Commands.RemovePermission;

public class RemovePermissionCommandHandler
    : IRequestHandler<RemovePermissionCommand>
{
    private readonly IApplicationDbContext _context;

    public RemovePermissionCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        RemovePermissionCommand request,
        CancellationToken cancellationToken)
    {
        var rolePermission = await _context.RolePermissions
            .FirstOrDefaultAsync(
                x =>
                    x.RoleId == request.RoleId &&
                    x.PermissionId == request.PermissionId,
                cancellationToken);

        if (rolePermission is null)
        {
            throw new KeyNotFoundException(
                "The permission is not assigned to this role.");
        }

        _context.RolePermissions.Remove(rolePermission);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
