using AppAPI.Attributes;
using AppAPI.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Auth;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {

            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationModel registration)
        {
            if (_authService.UserExists(registration))
            {
                return Conflict("User exists");
            }

            var registerResult = _authService.Register(registration, "Admin");

            var user = registerResult.Value;

            var claims = _authService.CreateClaims(user);
            var JwtToken = _authService.CreateJwt(claims, _configuration.GetValue<string>("Auth:SecretKey"));

            return Ok(new { Token = JwtToken });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var authenticationResult = _authService.Authenticate(login);
            
            if(!authenticationResult.IsOk)
            {
                return Unauthorized(authenticationResult.Error);
            }
            var user = authenticationResult.Value;

            var claims = _authService.CreateClaims(user);
            var JwtToken = _authService.CreateJwt(claims, _configuration.GetValue<string>("Auth:SecretKey"));

            return Ok(new { Token = JwtToken });
        }
    }
}
