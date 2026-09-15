using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Permissions.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Permissions.Queries.GetPermissionById;

public class GetPermissionByIdQueryHandler
    : IRequestHandler<
        GetPermissionByIdQuery,
        PermissionResponse>
{
    private readonly IApplicationDbContext _context;

    public GetPermissionByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PermissionResponse> Handle(
        GetPermissionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var permission = await _context.Permissions
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new PermissionResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (permission is null)
        {
            throw new KeyNotFoundException(
                $"Permission with ID '{request.Id}' was not found.");
        }

        return permission;
    }
}
