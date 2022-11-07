namespace EmployeeService.Models.Requests
{
    public class CreateEmployeeTypeRequest
    {
        public int Id { get; set; } //Идентификатор категории
        public string Description { get; set; } //Описание (название)
    }
}
