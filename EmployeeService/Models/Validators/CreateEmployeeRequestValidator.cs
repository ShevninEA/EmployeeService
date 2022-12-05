using EmployeeService.Models.Requests;
using FluentValidation;

namespace EmployeeService.Models.Validators
{
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty();

            RuleFor(a => a.DepartmentId)
                .NotEmpty();

            RuleFor(a => a.EmployeeTypeId)
                .NotEmpty();

            RuleFor(a => a.FirstName)
                .NotNull()
                .Length(3, 20)
                .ToString();

            RuleFor(a => a.Surname)
                .NotNull()
                .Length(3, 20)
                .ToString();

            RuleFor(a => a.Patronymic)
                .NotNull()
                .Length(3, 20)
                .ToString();

            RuleFor(a => a.Salary)
                .NotEmpty();
        }
    }
}
