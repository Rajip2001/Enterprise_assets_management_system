using EAMS.Application.Features.Authentication.DTOs;
using MediatR;

namespace EAMS.Application.Features.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(
    string RefreshToken
) : IRequest<RefreshTokenResponse>;
