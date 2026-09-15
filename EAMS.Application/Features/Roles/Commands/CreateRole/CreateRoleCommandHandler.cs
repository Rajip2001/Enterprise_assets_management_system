using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Roles.DTOs;
using EAMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler
    : IRequestHandler<CreateRoleCommand, RoleResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateRoleCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RoleResponse> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var roleName = request.Name.Trim();

        var exists = await _context.Roles
            .AnyAsync(
                x => x.Name.ToLower() == roleName.ToLower(),
                cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException(
                $"Role '{roleName}' already exists.");
        }

        var role = new Role
        {
            Id = Guid.NewGuid(),
            Name = roleName,
            Description = request.Description?.Trim()
        };

        _context.Roles.Add(role);

        await _context.SaveChangesAsync(cancellationToken);

        return new RoleResponse
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            UserCount = 0
        };
    }
}
