using System;
using System.Collections.Generic;
using System.Text;

using EAMS.Application.Features.Authentication.DTOs;
using MediatR;

namespace EAMS.Application.Features.Authentication.Commands.Login;

public record LoginCommand(
    string EmailOrUserName,
    string Password
) : IRequest<AuthResponse>;