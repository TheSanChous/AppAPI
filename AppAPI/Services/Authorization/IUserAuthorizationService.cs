using System.Security.Claims;
using Data.Models.Auth;

namespace AppAPI.Services.Authorization
{
    public interface IUserAuthorizationService
    {
        public int GetUserId(ClaimsPrincipal user);

        public User GetUser(ClaimsPrincipal user);
    }
}
