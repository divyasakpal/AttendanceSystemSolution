using AttendanceSystem.Shared.DTOs;
using FluentValidation;

namespace AttendanceSystem.Shared.Validators
{
   public class AttendanceValidator : AbstractValidator<AttendanceDto>
    {
        public AttendanceValidator()
        {
            RuleFor(x => x.EmployeeId).Must(x =>x!=0).WithMessage("Employee Id required.");
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.ClockTime).LessThanOrEqualTo(DateTime.Now).WithMessage("You entered a date and time in the future."); ;
        }
    }
}