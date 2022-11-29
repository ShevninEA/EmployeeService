using EmployeeService.Models;
using EmployeeService.Models.Requests;

namespace EmployeeService.Services
{
    public interface IAuthentificateService
    {
        AuthentificationResponse Login(AuthentificationRequest authentificationRequest);
        public SessionDto GetSession(string sessionToken);
    }
}
