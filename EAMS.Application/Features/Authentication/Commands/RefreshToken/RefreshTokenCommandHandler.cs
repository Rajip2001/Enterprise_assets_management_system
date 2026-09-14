using EAMS.Application.Common.Interfaces;
using EAMS.Application.Features.Authentication.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Features.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler
    : IRequestHandler<
        RefreshTokenCommand,
        RefreshTokenResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;

    public RefreshTokenCommandHandler(
        IApplicationDbContext context,
        IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<RefreshTokenResponse> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var storedToken = await _context.RefreshTokens
            .Include(x => x.User)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(
                x => x.Token == request.RefreshToken,
                cancellationToken);

        if (storedToken is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid refresh token.");
        }

        if (storedToken.IsRevoked)
        {
            throw new UnauthorizedAccessException(
                "Refresh token has been revoked.");
        }

        if (storedToken.ExpiresAt <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException(
                "Refresh token has expired.");
        }

        if (!storedToken.User.IsActive)
        {
            throw new UnauthorizedAccessException(
                "User account is inactive.");
        }

        var user = storedToken.User;

        // Revoke old refresh token
        storedToken.IsRevoked = true;

        // Generate new tokens
        var accessToken =
            _jwtService.GenerateAccessToken(user);

        var newRefreshToken =
            _jwtService.GenerateRefreshToken();

        var newRefreshTokenExpiresAt =
            DateTime.UtcNow.AddDays(
                _jwtService.GetRefreshTokenExpirationDays());

        var refreshTokenEntity = new Domain.Entities.RefreshToken
        {
            Id = Guid.NewGuid(),

            Token = newRefreshToken,

            UserId = user.Id,

            ExpiresAt = newRefreshTokenExpiresAt,

            IsRevoked = false
        };

        await _context.RefreshTokens.AddAsync(
            refreshTokenEntity,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);

        return new RefreshTokenResponse
        {
            AccessToken = accessToken,

            AccessTokenExpiresAt =
                DateTime.UtcNow.AddMinutes(
                    _jwtService
                        .GetAccessTokenExpirationMinutes()),

            RefreshToken = newRefreshToken,

            RefreshTokenExpiresAt =
                newRefreshTokenExpiresAt
        };
    }
}