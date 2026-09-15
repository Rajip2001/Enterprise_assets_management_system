using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Roles.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Roles.Queries.GetRoles;

public class GetRolesQueryHandler
    : IRequestHandler<GetRolesQuery, List<RoleResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetRolesQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoleResponse>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .Select(role => new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                UserCount = role.Users.Count
            })
            .ToListAsync(cancellationToken);
    }
}
