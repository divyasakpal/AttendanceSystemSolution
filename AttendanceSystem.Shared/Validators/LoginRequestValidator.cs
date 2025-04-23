using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Shared.DTOs;
using FluentValidation;

namespace AttendanceSystem.Shared.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(" * required.")
                            .EmailAddress().WithMessage("Username is not in valid email format");
            RuleFor(x => x.Password).NotNull().WithMessage(" * required.");
        }
    }
}
