using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<bool> IsValidUser(string username, string password)
        {
            var result = await _repository.GetUserAsync(username);

            if (result == null)
            {
                return false;
            }

            var passwordHasher = new PasswordHasher<object>();

            var verificationResult = passwordHasher.VerifyHashedPassword(null, result.PasswordHash, password);

            var resultVerify = verificationResult == PasswordVerificationResult.Success && username == result.UserName;
            //password123
            return resultVerify;
        }


        public async Task<string> GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value));                                                                         
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await _repository.GetRolesAsync(username);
            
            var userRole = roles.Role.Name;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, userRole),

            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44368",
                audience: "https://localhost:44368",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> GetRolesAsync(string userName) => await _repository.GetRolesAsync(userName);
         
    }
}