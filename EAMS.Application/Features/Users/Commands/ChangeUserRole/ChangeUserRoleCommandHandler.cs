using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Users.Commands.ChangeUserRole;

public class ChangeUserRoleCommandHandler
    : IRequestHandler<ChangeUserRoleCommand, UserResponse>
{
    private readonly IApplicationDbContext _context;

    public ChangeUserRoleCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserResponse> Handle(
        ChangeUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(
                x => x.Id == request.UserId,
                cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException(
                $"User with ID '{request.UserId}' was not found.");
        }

        var role = await _context.Roles
            .FirstOrDefaultAsync(
                x => x.Id == request.RoleId,
                cancellationToken);

        if (role is null)
        {
            throw new KeyNotFoundException(
                $"Role with ID '{request.RoleId}' was not found.");
        }

        user.RoleId = role.Id;

        await _context.SaveChangesAsync(cancellationToken);

        return new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            IsActive = user.IsActive,
            RoleId = role.Id,
            Role = role.Name,
            LastLogin = user.LastLogin
        };
    }
}
