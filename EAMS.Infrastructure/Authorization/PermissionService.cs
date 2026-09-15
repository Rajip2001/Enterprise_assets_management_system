using EAMS.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using EAMS.Infrastructure.Persistence.Context;

namespace EAMS.Infrastructure.Authorization;

public class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _context;

    public PermissionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasPermissionAsync(
        Guid userId,
        string permission,
        CancellationToken cancellationToken = default)
    {
        return await _context.RolePermissions
            .AnyAsync(
                rp =>
                    rp.Role.Users.Any(u => u.Id == userId) &&
                    rp.Permission.Name == permission,
                cancellationToken);
    }
}
