using Data.Repositories;
using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppAPI.Services.Autorization
{
    public class UserAutorizationService : ServiceBase, IUserAutorizationService
    {
        private readonly IUserRepository _userRepository;

        public UserAutorizationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IServiceActionResult<User> GetUser(ClaimsPrincipal User)
        {
            var user = _userRepository.Get(GetUserId(User).Value);
            if(user is null)
            {
                return Error<User>("User not found", null);
            }

            return Ok(user);
        }

        public IServiceActionResult<int> GetUserId(ClaimsPrincipal User)
        {
            var user = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(user);
        }
    }
}
