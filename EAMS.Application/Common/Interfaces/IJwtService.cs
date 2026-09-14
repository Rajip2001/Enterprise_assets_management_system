using System;
using System.Collections.Generic;
using System.Text;

using EAMS.Domain.Entities;

namespace EAMS.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);
}
