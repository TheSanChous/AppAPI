using AuthAPI.Attributes;
using AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
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
        public IActionResult Register([FromBody] Registration registration)
        {
            if (_authService.UserExists(registration))
            {
                return Conflict();
            }

            var user = _authService.Register(registration, "Admin");

            var claims = _authService.CreateClaims(user);
            var JwtToken = _authService.CreateJwt(claims, _configuration.GetValue<string>("Auth:SecretKey"));

            return Ok(new { Token = JwtToken });
        }

        [HttpPost("login")]
        public IActionResult Register([FromBody] Login login)
        {
            var user = _authService.Authenticate(login);
            
            if(user is null)
            {
                return Unauthorized();
            }

            var claims = _authService.CreateClaims(user);
            var JwtToken = _authService.CreateJwt(claims, _configuration.GetValue<string>("Auth:SecretKey"));

            return Ok(new { Token = JwtToken });
        }

        [HttpGet("extended")]
        [RequirePermission(PermissionType.AccessExtended)]
        public IActionResult GetExtended()
        {
            return Ok();
        }
    }
}
