using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Users.DTOs;
using EAMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponse> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var userName = request.UserName.Trim();

        var emailExists = await _context.Users
            .AnyAsync(
                x => x.Email.ToLower() == email,
                cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        var userNameExists = await _context.Users
            .AnyAsync(
                x => x.UserName.ToLower() == userName.ToLower(),
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

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = email,
            UserName = userName,
            PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber)
                ? null
                : request.PhoneNumber.Trim(),
            PasswordHash = _passwordHasher.Hash(request.Password),
            IsActive = true,
            RoleId = role.Id
        };

        _context.Users.Add(user);

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
