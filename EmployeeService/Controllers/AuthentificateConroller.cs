using EmployeeService.Models;
using EmployeeService.Models.Requests;
using EmployeeService.Models.Validators;
using EmployeeService.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace EmployeeService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificateConroller : ControllerBase
    {
        private readonly IAuthentificateService _authentificateService;

        private readonly IValidator<AuthentificationRequest> _authentificationRequestValidator;

        public AuthentificateConroller(IAuthentificateService authentificateService,
            IValidator<AuthentificationRequest> authentificationRequestValidator)
        {
            _authentificateService = authentificateService;
            _authentificationRequestValidator = authentificationRequestValidator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthentificationResponse), StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] AuthentificationRequest authentificationRequest) 
        {
            ValidationResult validationResult = _authentificationRequestValidator.Validate(authentificationRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            AuthentificationResponse authentificationResponse = _authentificateService.Login(authentificationRequest);
            if (authentificationResponse.Status == Models.AuthentificationStatus.Success)
            {
                Response.Headers.Add("X-Session-Token", authentificationResponse.Session.SessionToken);
            }
            return Ok(authentificationResponse);
        }

        [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]

        [HttpGet]
        [Route("session")]
        public IActionResult GetSession()
        {
            var authorizationHeader = Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
            {
                var schema = headerValue.Scheme;
                var sessionToken = headerValue.Parameter;

                if (string.IsNullOrEmpty(sessionToken))
                    return Unauthorized();

                SessionDto sessionDto = _authentificateService.GetSession(sessionToken);
                if (sessionToken == null)
                    return Unauthorized();

                return Ok(sessionDto);
            }
            return Unauthorized();
        }
    }
}
