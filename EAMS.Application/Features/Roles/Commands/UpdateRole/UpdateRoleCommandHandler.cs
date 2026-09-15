using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Roles.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler
    : IRequestHandler<UpdateRoleCommand, RoleResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateRoleCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RoleResponse> Handle(
        UpdateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (role is null)
        {
            throw new KeyNotFoundException(
                $"Role with ID '{request.Id}' was not found.");
        }

        var roleName = request.Name.Trim();

        var duplicate = await _context.Roles
            .AnyAsync(
                x =>
                    x.Id != request.Id &&
                    x.Name.ToLower() == roleName.ToLower(),
                cancellationToken);

        if (duplicate)
        {
            throw new InvalidOperationException(
                $"Role '{roleName}' already exists.");
        }

        role.Name = roleName;
        role.Description = request.Description?.Trim();

        await _context.SaveChangesAsync(cancellationToken);

        return new RoleResponse
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            UserCount = await _context.Users
                .CountAsync(
                    x => x.RoleId == role.Id,
                    cancellationToken)
        };
    }
}
