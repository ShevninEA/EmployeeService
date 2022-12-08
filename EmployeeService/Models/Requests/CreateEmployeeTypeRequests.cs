namespace EmployeeService.Models.Requests
{
    public class CreateEmployeeTypeRequests
    {
        public int Id { get; set; } //Идентификатор категории
        public string Description { get; set; } //Описание (название)
    }
}
