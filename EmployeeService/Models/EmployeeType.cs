namespace EmployeeService.Models
{
    /// <summary>
    /// Категория рабочего (фрилансер например)
    /// </summary>
    public class EmployeeType
    {
        public int Id { get; set; } //Идентификатор категории
        public string Description { get; set; } //Описание (название)
    }
}
