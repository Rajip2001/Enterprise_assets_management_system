using EAMS.Domain.Entities;

namespace EAMS.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    int GetAccessTokenExpirationMinutes();

    string GenerateRefreshToken();

    int GetRefreshTokenExpirationDays();
}
