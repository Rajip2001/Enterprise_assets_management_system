using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Authentication.DTOs;
using EAMS.Domain.Common;
using EAMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResponse> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var userName = request.UserName.Trim();

        // Check duplicate email
        var emailExists = await _context.Users
            .AnyAsync(x => x.Email == email, cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        // Check duplicate username
        var userNameExists = await _context.Users
            .AnyAsync(x => x.UserName == userName, cancellationToken);

        if (userNameExists)
        {
            throw new InvalidOperationException(
                "This username is already taken.");
        }

        // Get default Employee role
        var employeeRole = await _context.Roles
            .FirstOrDefaultAsync(
                x => x.Name == Roles.Employee,
                cancellationToken);

        if (employeeRole is null)
        {
            throw new InvalidOperationException(
                "Default Employee role was not found.");
        }

        // Create user
        var user = new User
        {
            Id = Guid.NewGuid(),

            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),

            Email = email,
            UserName = userName,

            PasswordHash = _passwordHasher.Hash(request.Password),

            PhoneNumber = request.PhoneNumber?.Trim(),

            IsActive = true,

            RoleId = employeeRole.Id
        };

        await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new RegisterResponse
        {
            UserId = user.Id,
            Message = "User registered successfully."
        };
    }
}
