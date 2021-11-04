using AppAPI.DTO.Auth;
using AppAPI.Models;
using Data;
using Data.Models.Auth;
using Data.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AppAPI.Services.Auth.Exceptions;

namespace AppAPI.Services.Auth
{
    public class AuthService : IAuthService
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

        public User Authenticate(LoginModel loginModel)
        {
            User user = _userRepository.Get(loginModel.Email);

            if (user is null)
            {
                throw new UserNotFoundException();
            }

            string hashedPassword = GetHashedPassword(loginModel.Password, user.Salt);

            if (!hashedPassword.Equals(user.HashedPassword))
            {
                throw new WrongPasswordException();
            }

            return user;
        }

        public User Register(RegistrationModel registrationModel)
        {
            if (UserExist(registrationModel))
            {
                throw new UserExistException();
            }

            var userRole = _roleRepository.Get("Admin");

            var salt = GetSalt(512 / 8);

            var hashedPass = GetHashedPassword(registrationModel.Password, salt);

            var user = new User
            {
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                Email = registrationModel.Email,
                Salt = salt,
                HashedPassword = hashedPass,
                Roles = new List<Role> { userRole }
            };

            _userRepository.Add(user);
            _context.SaveChanges();

            return user;
        }

        public string GetJwtToken(User user, string signingKey)
        {
            var claims = CreateClaims(user);
            var token = CreateJwt(claims, signingKey);
            return token;
        }

        private List<Claim> CreateClaims(User user)
        {
            var defaultClaims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer),
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Name, user.FirstName),
                new (ClaimTypes.Surname, user.LastName)
            };

            var permissionClaims = user.Roles
                .SelectMany(r => r.Permissions)
                .Distinct()
                .Select(p => new Claim("permission", p.Title));

            return defaultClaims.Concat(permissionClaims).ToList();
        }

        private string CreateJwt(IEnumerable<Claim> claims, string signingKey)
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

        private bool UserExist(RegistrationModel registration)
        {
            return _userRepository.Get(registration.Email) is not null;
        }

        private byte[] GetSalt(int size)
        {
            var salt = new byte[size];
            _cryptoServiceProvider.GetNonZeroBytes(salt);
            return salt;
        }

        private static string GetHashedPassword(string password, byte[] salt)
        {
            return Convert
                .ToBase64String(KeyDerivation
                .Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, 100_000, 1024 / 8));
        }
    }
}
