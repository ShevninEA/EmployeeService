namespace EmployeeService.Models.Requests
{
    public class CreateEmployeeRequest
    {
        public int Id { get; set; } //Идентификатор
        public int DepartmentId { get; set; } //Идентификатор департамента, к которому он пренадлежит (определенный класс рабочих)
        public int EmployeeTypeId { get; set; } //Идентификатор категория рабочего (фрилансер например)
        public string FirstName { get; set; } //Имя
        public string Surname { get; set; } //Фамилия
        public string Patronymic { get; set; } //Отчество
        public decimal Salary { get; set; } //Зарплата
    }
}
