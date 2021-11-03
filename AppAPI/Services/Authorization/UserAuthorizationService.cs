using Data.Models.Auth;
using Data.Repositories;
using System.Security.Claims;

namespace AppAPI.Services.Authorization
{
    public class UserAuthorizationService : ServiceBase, IUserAuthorizationService
    {
        private readonly IUserRepository _userRepository;

        public UserAuthorizationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IServiceActionResult<User> GetUser(ClaimsPrincipal userClaims)
        {
            var user = _userRepository.Get(GetUserId(userClaims).Value);
            if(user is null)
            {
                return Error<User>("User not found", null);
            }

            return Ok(user);
        }

        public IServiceActionResult<int> GetUserId(ClaimsPrincipal userClaims)
        {
            var user = int.Parse(userClaims.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(user);
        }
    }
}
