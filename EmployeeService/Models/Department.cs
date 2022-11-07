namespace EmployeeService.Models
{
    /// <summary>
    /// Определенный класс рабочих
    /// </summary>
    public class Department
    {
        public Guid Id { get; set; } //Идентификатор категории
        public string Description { get; set; } //Описание (название)
    }
}
