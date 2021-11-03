using System.Security.Claims;
using Data.Models.Auth;

namespace AppAPI.Services.Authorization
{
    public interface IUserAuthorizationService
    {
        public IServiceActionResult<int> GetUserId(ClaimsPrincipal user);

        public IServiceActionResult<User> GetUser(ClaimsPrincipal user);
    }
}
