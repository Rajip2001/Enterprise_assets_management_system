using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Roles.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler
    : IRequestHandler<GetRoleByIdQuery, RoleResponse>
{
    private readonly IApplicationDbContext _context;

    public GetRoleByIdQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RoleResponse> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new RoleResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UserCount = x.Users.Count
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (role is null)
        {
            throw new KeyNotFoundException(
                $"Role with ID '{request.Id}' was not found.");
        }

        return role;
    }
}