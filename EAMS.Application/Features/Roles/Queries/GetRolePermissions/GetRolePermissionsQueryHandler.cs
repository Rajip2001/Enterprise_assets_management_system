using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Roles.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Roles.Queries.GetRolePermissions;

public class GetRolePermissionsQueryHandler
    : IRequestHandler<
        GetRolePermissionsQuery,
        List<RolePermissionResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetRolePermissionsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RolePermissionResponse>> Handle(
        GetRolePermissionsQuery request,
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

        return await _context.RolePermissions
            .AsNoTracking()
            .Where(x => x.RoleId == request.RoleId)
            .Select(x => new RolePermissionResponse
            {
                RoleId = x.RoleId,
                RoleName = x.Role.Name,
                PermissionId = x.PermissionId,
                PermissionName = x.Permission.Name,
                PermissionDescription = x.Permission.Description
            })
            .OrderBy(x => x.PermissionName)
            .ToListAsync(cancellationToken);
    }
}
