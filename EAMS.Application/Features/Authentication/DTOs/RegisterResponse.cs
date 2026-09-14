using System;
using System.Collections.Generic;
using System.Text;

namespace EAMS.Application.Features.Authentication.DTOs;

public class RegisterResponse
{
    public Guid UserId { get; set; }

    public string Message { get; set; } = string.Empty;
}