using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;

namespace EAMS.Application.Features.Authentication.Commands.Login;

public class LoginCommandValidator
    : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.EmailOrUserName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
