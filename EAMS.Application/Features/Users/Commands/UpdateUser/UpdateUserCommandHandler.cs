using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, UserResponse>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserResponse> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException(
                $"User with ID '{request.Id}' was not found.");
        }

        var email = request.Email
            .Trim()
            .ToLowerInvariant();

        var userName = request.UserName.Trim();

        var emailExists = await _context.Users
            .AnyAsync(
                x => x.Id != request.Id &&
                     x.Email.ToLower() == email,
                cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        var userNameExists = await _context.Users
            .AnyAsync(
                x => x.Id != request.Id &&
                     x.UserName.ToLower() == userName.ToLower(),
                cancellationToken);

        if (userNameExists)
        {
            throw new InvalidOperationException(
                "A user with this username already exists.");
        }

        var role = await _context.Roles
            .FirstOrDefaultAsync(
                x => x.Id == request.RoleId,
                cancellationToken);

        if (role is null)
        {
            throw new KeyNotFoundException(
                "The specified role was not found.");
        }

        user.FirstName = request.FirstName.Trim();
        user.LastName = request.LastName.Trim();
        user.Email = email;
        user.UserName = userName;
        user.PhoneNumber =
            string.IsNullOrWhiteSpace(request.PhoneNumber)
                ? null
                : request.PhoneNumber.Trim();

        user.RoleId = role.Id;
        user.IsActive = request.IsActive;

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