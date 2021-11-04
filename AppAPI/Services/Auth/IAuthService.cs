using Models.Auth;
using System.Collections.Generic;
using System.Security.Claims;
using AppAPI.DTO.Auth;
using Data.Models.Auth;

namespace AppAPI.Services.Auth
{
    public interface IAuthService
    {
        public User Authenticate(LoginModel loginModel);

        public string GetJwtToken(User user, string signingKey);

        public User Register(RegistrationModel registration);
    }
}
