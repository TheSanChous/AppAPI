using Data.Repositories;
using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppAPI.Services.Autorization
{
    public class UserAutorizationService : IUserAutorizationService
    {
        private readonly IUserRepository _userRepository;

        public UserAutorizationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(ClaimsPrincipal User)
        {
            return _userRepository.Get(GetUserId(User));
        }

        public int GetUserId(ClaimsPrincipal User)
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
