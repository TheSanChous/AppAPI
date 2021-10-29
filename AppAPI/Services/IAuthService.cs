using Models;
using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthAPI.Services
{
    public interface IAuthService
    {
        public User Authenticate(Login login);

        public ClaimsPrincipal CreatePrincipal(User user);

        public List<Claim> CreateClaims(User user);

        string CreateJwt(IEnumerable<Claim> claims, string signingKey);

        User Register(Registration registration, string asRole);

        public bool UserExists(Registration registration);
    }
}
