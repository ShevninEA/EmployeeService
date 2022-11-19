namespace EmployeeService.Models.Requests
{
    public class AuthentificationResponse
    {
        public AuthentificationStatus Status { get; set; }
        public SessionDto Session { get; set; }
    }
}
