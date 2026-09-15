using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Permissions.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Permissions.Queries.GetPermissions;

public class GetPermissionsQueryHandler
    : IRequestHandler<
        GetPermissionsQuery,
        List<PermissionResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetPermissionsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PermissionResponse>> Handle(
        GetPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new PermissionResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            })
            .ToListAsync(cancellationToken);
    }
}
