using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Shared.DTOs;
using FluentValidation;

namespace AttendanceSystem.Shared.Validators
{
   public class RegistorRequestValidator : AbstractValidator<RegisterDto>
    {
        public RegistorRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("* required.")
                            .EmailAddress().WithMessage("Username is not valid.");
            RuleFor(x => x.Password).NotNull();
            RuleFor(x => x.Roles).NotEmpty().WithMessage("Roles must be in Reader and Writer."); ;
        }
    }
}