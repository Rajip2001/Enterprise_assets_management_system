using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Authentication.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAMS.Application.Features.Authentication.Commands.Login;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var loginValue = request.EmailOrUserName
            .Trim();

        var normalizedLoginValue =
            loginValue.ToLowerInvariant();

        // Find user by Email OR UserName
        var user = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(
                x =>
                    x.Email.ToLower() == normalizedLoginValue ||
                    x.UserName.ToLower() == normalizedLoginValue,
                cancellationToken);

        // Do not reveal whether user exists
        if (user is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid username/email or password.");
        }

        // Check account status
        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException(
                "This account is inactive.");
        }

        // Verify password
        var passwordValid =
            _passwordHasher.Verify(
                request.Password,
                user.PasswordHash);

        if (!passwordValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid username/email or password.");
        }

        // Update last login
        user.LastLogin = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        // Generate JWT
        var accessToken =
            _jwtService.GenerateAccessToken(user);

        // Calculate expiry
        var expiresAt =
            DateTime.UtcNow.AddMinutes(15);

        return new AuthResponse
        {
            UserId = user.Id,

            FirstName = user.FirstName,
            LastName = user.LastName,

            Email = user.Email,
            UserName = user.UserName,

            Role = user.Role.Name,

            AccessToken = accessToken,

            AccessTokenExpiresAt = expiresAt
        };
    }
}
