using EAMS.Application.Common.Interfaces;
using EAMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Roles.Commands.AssignPermission;

public class AssignPermissionCommandHandler
    : IRequestHandler<AssignPermissionCommand>
{
    private readonly IApplicationDbContext _context;

    public AssignPermissionCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        AssignPermissionCommand request,
        CancellationToken cancellationToken)
    {
        var roleExists = await _context.Roles
            .AnyAsync(
                x => x.Id == request.RoleId,
                cancellationToken);

        if (!roleExists)
        {
            throw new KeyNotFoundException(
                $"Role with ID '{request.RoleId}' was not found.");
        }

        var permissionExists = await _context.Permissions
            .AnyAsync(
                x => x.Id == request.PermissionId,
                cancellationToken);

        if (!permissionExists)
        {
            throw new KeyNotFoundException(
                $"Permission with ID '{request.PermissionId}' was not found.");
        }

        var alreadyAssigned = await _context.RolePermissions
            .AnyAsync(
                x =>
                    x.RoleId == request.RoleId &&
                    x.PermissionId == request.PermissionId,
                cancellationToken);

        if (alreadyAssigned)
        {
            throw new InvalidOperationException(
                "This permission is already assigned to the role.");
        }

        var rolePermission = new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = request.RoleId,
            PermissionId = request.PermissionId
        };

        _context.RolePermissions.Add(rolePermission);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
