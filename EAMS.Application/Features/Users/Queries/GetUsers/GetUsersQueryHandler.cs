using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Users.Queries.GetUsers;

public class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, List<UserResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserResponse>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .Select(x => new UserResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                UserName = x.UserName,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive,
                RoleId = x.RoleId,
                Role = x.Role.Name,
                LastLogin = x.LastLogin
            })
            .ToListAsync(cancellationToken);
    }
}
