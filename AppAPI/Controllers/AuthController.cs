using System;
using AppAPI.DTO.Auth;
using AppAPI.Services.Auth;
using AppAPI.Services.Auth.Exceptions;
using Data.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AppAPI.Controllers
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
        public IActionResult Register([FromBody] RegistrationModel registrationModel)
        {
            try
            {
                User user = _authService.Register(registrationModel);

                string token = _authService.GetJwtToken(user, _configuration.GetValue<string>("Auth:SecretKey"));

                var response = new
                {
                    Token = token
                };

                return Ok(response);
            }
            catch (UserExistException)
            {
                var response = new
                {
                    message = "Email is already exist"
                };
                return Unauthorized(response);
            }
            catch (Exception exception)
            {
                var response = new
                {
                    message = exception.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                User user = _authService.Authenticate(loginModel);

                string token = _authService.GetJwtToken(user, _configuration.GetValue<string>("Auth:SecretKey"));

                var response = new
                {
                    Token = token
                };

                return Ok(response);
            }
            catch (UserNotFoundException)
            {
                var response = new
                {
                    message = "User not found"
                };
                return Unauthorized(response);
            }
            catch (WrongPasswordException)
            {
                var response = new
                {
                    message = "Wrong password"
                };
                return Unauthorized(response);
            }
            catch (Exception exception)
            {
                var response = new
                {
                    message = exception.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
