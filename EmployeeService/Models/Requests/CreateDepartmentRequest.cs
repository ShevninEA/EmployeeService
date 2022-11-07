namespace EmployeeService.Models.Requests
{
    public class CreateDepartmentRequest
    {
        public Guid Id { get; set; } //Идентификатор категории
        public string Description { get; set; } //Описание (название)
    }
}
