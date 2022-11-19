using EmployeeService.Models;
using EmployeeService.Models.Requests;

namespace EmployeeService.Services
{
    public interface IAuthentificateService
    {
        AuthentificationResponse Login(AuthentificationRequest authentificationResponse);
        public SessionDto GetSession(string sessionToken);
    }
}
