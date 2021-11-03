using Models.Auth;
using System.Collections.Generic;
using System.Security.Claims;

namespace AppAPI.Services.Auth
{
    public interface IAuthService
    {
        public IServiceActionResult<User> Authenticate(Login login);

        public ClaimsPrincipal CreatePrincipal(User user);

        public List<Claim> CreateClaims(User user);

        string CreateJwt(IEnumerable<Claim> claims, string signingKey);

        public IServiceActionResult<User> Register(RegistrationModel registration, string asRole);

        public bool UserExists(RegistrationModel registration);
    }
}
