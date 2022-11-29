using EmployeeService.Models.Requests;
using EmployeeService.Services;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificateConroller : ControllerBase
    {
        private readonly IAuthentificateService _authentificateService;

        public AuthentificateConroller(IAuthentificateService authentificateService)
        {
            _authentificateService = authentificateService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] AuthentificationRequest authentificationRequest) 
        {
            AuthentificationResponse authentificationResponse = _authentificateService.Login(authentificationRequest);
            if (authentificationResponse.Status == Models.AuthentificationStatus.Success)
            {
                Response.Headers.Add("X-Session-Token", authentificationResponse.Session.SessionToken);
            }
            return Ok(authentificationResponse);
        }
    }
}
