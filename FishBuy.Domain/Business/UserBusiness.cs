using FishBuy.Domain.Contracts;
using FishBuy.Domain.Entities;
using FishBuy.Domain.Helpers;
using FishBuy.Domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FishBuy.Domain.Business
{
    public class UserBusiness : IUserBusiness
    {

        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;


        public UserBusiness(IUserRepository userRepository, IOptions<AppSettings> appSettings)

        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return tokenString;

        }

        public User AuthenticateUser(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            // check if user exists
            if (user == null)
                return null;

            // check if password is correct
            var messages = VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
            if (messages != null)
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public List<string> CreateUser(User user, string password)
        {
            if (_userRepository.GetUserByEmail(user.Email) != null)
                return new List<string> { "This email is already registered" };

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.Add(user);

            return null;
        }

        public void UpdateUser(User userParam, string password = null)
        {
            var user = _userRepository.GetById(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
            {
                // throw error if the new username is already taken
                if (_userRepository.GetUserByEmail(userParam.Email) != null)
                    throw new AppException("Email " + userParam.Email + " is already taken");

                user.Email = userParam.Email;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            user.Administrator = userParam.Administrator;

            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                _userRepository.Delete(user);
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static List<string> VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) return new List<string> { "Password is required" };
            if (string.IsNullOrWhiteSpace(password)) return new List<string> { "Value cannot be empty or whitespace only string." };
            if (storedHash.Length != 64) return new List<string> { "Invalid length of password hash (64 bytes expected)." };
            if (storedSalt.Length != 128) return new List<string> { "Invalid length of password salt (128 bytes expected)." };

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return new List<string> { "Computed Password Hash and Stored Password Hash do not match." };
                }
            }

            return null;
        }
    }

}
