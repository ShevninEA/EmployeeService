using EmployeeService.Data;
using EmployeeService.Models;
using EmployeeService.Models.Requests;
using EmployeeService.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeService.Services.Impl
{
    public class AuthentificateService : IAuthentificateService
    {
        public const string SecretKey = "kYp3s6v9y/B?E(H+";

        private readonly Dictionary<string, SessionDto> _session = new Dictionary<string, SessionDto>();

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AuthentificateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public AuthentificationResponse Login(AuthentificationRequest authentificationRequest)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            EmployeeServiceDbContext context = scope.ServiceProvider.GetRequiredService<EmployeeServiceDbContext>();

            Account account =
                !string.IsNullOrWhiteSpace(authentificationRequest.Login) ?
                FindAccountByLogin(context, authentificationRequest.Login) : null;

            if (account == null)
            {
                return new AuthentificationResponse
                {
                    Status = AuthentificationStatus.UserNotFound
                };
            }

            if (!PasswordUtils.VerifyPassword(authentificationRequest.Password, account.PasswordSalt, account.PasswordHash))
            {
                return new AuthentificationResponse
                {
                    Status = AuthentificationStatus.InvalidPassword
                };
            }

            AccountSession session = new AccountSession
            {
                AccountId = account.AccountId,
                SessionToken = CreateSessionToken(account),
                TimeCreated = DateTime.Now,
                TimeClosed = DateTime.Now,
                IsClosed = false
            };

            context.AccountSession.Add(session);
            context.SaveChanges();

            SessionDto sessionDto = GetSessionDto(account, session);
            lock (_session) 
            {
                _session[session.SessionToken] = sessionDto;
            }

            return new AuthentificationResponse
            {
                Status = AuthentificationStatus.Success,
                Session = sessionDto
            };
        }

        private SessionDto GetSessionDto(Account account, AccountSession accountSession) 
        {
            return new SessionDto
            {
                SessionId = accountSession.SessionId,
                SessionToken = accountSession.SessionToken,
                Account = new AccountDto
                {
                    AccountId = account.AccountId,
                    EMail = account.EMail,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    SecondName = account.SecondName,
                    Locked = account.Locked
                }
            };
        }

        private string CreateSessionToken(Account account)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,account.AccountId.ToString()),
                        new Claim(ClaimTypes.Name, account.EMail),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private Account FindAccountByLogin(EmployeeServiceDbContext context, string login) 
        {
            return context.Account.FirstOrDefault(account => account.EMail == login);
        }
        public SessionDto GetSession(string sessionToken)
        {
            SessionDto sessionDto;
            lock (_session)
            {
                _session.TryGetValue(sessionToken, out sessionDto);
            }

            if (sessionDto == null)
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();
                EmployeeServiceDbContext context = scope.ServiceProvider.GetRequiredService<EmployeeServiceDbContext>();

                AccountSession session = context
                    .AccountSession
                    .FirstOrDefault(item => item.SessionToken == sessionToken);

                if (session == null)
                    return null;

                Account account = context.Account.FirstOrDefault(item => item.AccountId == session.AccountId);
                sessionDto = GetSessionDto(account, session);

                if (sessionDto != null)
                {
                    lock (_session)
                    {
                        _session[sessionToken] = sessionDto;
                    }
                }
            }
            return sessionDto;
        }
    }
}
