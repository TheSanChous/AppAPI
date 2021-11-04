using Data.Models.Auth;
using Data.Repositories;
using System.Security.Claims;
using AppAPI.Services.Authorization.Exceptions;

namespace AppAPI.Services.Authorization
{
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private readonly IUserRepository _userRepository;

        public UserAuthorizationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(ClaimsPrincipal userClaims)
        {
            var user = _userRepository.Get(GetUserId(userClaims));
            
            if(user is null)
            {
                throw new InvalidClaimsException();
            }

            return user;
        }

        public int GetUserId(ClaimsPrincipal userClaims)
        {
            string nameIdentifier = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrEmpty(nameIdentifier))
            {
                throw new InvalidClaimsException();
            }

            int userId = int.Parse(nameIdentifier);

            return userId;
        }
    }
}
