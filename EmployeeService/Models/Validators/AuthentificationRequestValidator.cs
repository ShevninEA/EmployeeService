using EmployeeService.Models.Requests;
using FluentValidation;

namespace EmployeeService.Models.Validators
{
    public class AuthentificationRequestValidator : AbstractValidator<AuthentificationRequest>
    {
        public AuthentificationRequestValidator()
        {
            RuleFor(a => a.Login)
                .NotNull()
                .Length(7, 255)
                .EmailAddress();

            RuleFor(a => a.Password)
                .NotNull()
                .Length(5, 30);
        }
    }
}
