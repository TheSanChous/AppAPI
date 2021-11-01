using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppAPI.Services.Auth
{
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        private readonly DatabaseContext _context;

        private readonly RNGCryptoServiceProvider _cryptoServiceProvider;

        public AuthService( 
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            DatabaseContext context,
            RNGCryptoServiceProvider cryptoServiceProvider)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _context = context;
            _cryptoServiceProvider = cryptoServiceProvider;
        }

        public IServiceActionResult<User> Authenticate(Login login)
        {
            User user = _userRepository.Get(login.Email);

            if (user is null)
            {
                return Error<User>("User not found", null);
            }

            string hashedPassword = GetHashedPassword(login.Password, user.Salt);

            if (!hashedPassword.Equals(user.HashedPassword))
            {
                return Error<User>("Wrong password", null);
            }

            return Ok(user);
        }

        public List<Claim> CreateClaims(User user)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var permissions = user.Roles
                .SelectMany(r => r.Permissions)
                .Distinct()
                .Select(p => new Claim("permission", p.Title));

            return Claims.Concat(permissions).ToList();
        }

        public string CreateJwt(IEnumerable<Claim> claims, string signingKey)
        {
            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                notBefore: now,
                expires: now.AddDays(365),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(signingKey)),
                    SecurityAlgorithms.HmacSha256)
                );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        public ClaimsPrincipal CreatePrincipal(User user)
        {
            var claims = CreateClaims(user);

            var identity = new ClaimsIdentity(claims);

            return new ClaimsPrincipal(identity);
        }

        public bool UserExists(Registration registration)
        {
            return _userRepository.Get(registration.Email) is not null;
        }

        public IServiceActionResult<User> Register(Registration registration, string asRole)
        {
            var userRole = _roleRepository.Get(asRole);

            var salt = new byte[512 / 8];
            _cryptoServiceProvider.GetNonZeroBytes(salt);

            var hashedPass = GetHashedPassword(registration.Password, salt);

            var user = new User
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Email = registration.Email,
                Salt = salt,
                HashedPassword = hashedPass,
                Roles = new List<Role> { userRole }
            };

            _userRepository.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        private static string GetHashedPassword(string password, byte[] salt)
        {
            return Convert
                .ToBase64String(KeyDerivation
                .Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, 100_000, 1024 / 8));
        }
    }
}
