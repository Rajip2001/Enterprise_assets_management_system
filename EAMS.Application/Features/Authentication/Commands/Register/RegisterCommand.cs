using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using EAMS.Application.Features.Authentication.DTOs;

namespace EAMS.Application.Features.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    string? PhoneNumber
) : IRequest<RegisterResponse>;
