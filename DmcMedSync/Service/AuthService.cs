using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DmcMedSync.Models;

namespace DmcMedSync.Service
{
    public class AuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        private static readonly List<User> _users = new()
        {
            new User { Username = "admin", Password = "12345", Role = "Admin" },
            new User { Username = "schoolnurse", Password = "12345", Role = "SchoolNurse" }
        };

        public User? ValidateUser(string username, string password)
        {
            return _users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }

        public (bool Success, string Message) RegisterUser(UserRegistration newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.Username) || string.IsNullOrWhiteSpace(newUser.Password))
            {
                return (false, "Username and password are required.");
            }

            bool exists = _users.Any(u =>
                u.Username.Equals(newUser.Username, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                return (false, $"Username '{newUser.Username}' is already taken.");
            }

            string assignedRole = string.IsNullOrWhiteSpace(newUser.Role) ? "SchoolNurse" : newUser.Role;

            _users.Add(new UserRegistration
            {
                Id = newUser.Id,
                FullName = newUser.FullName,
                DateOfBirth = newUser.DateOfBirth,
                HomeAddress = newUser.HomeAddress,
                PhoneNumber = newUser.PhoneNumber,
                Email = newUser.Email,
                Username = newUser.Username.Trim(),
                Password = newUser.Password,
                Role = assignedRole
            });

            return (true, $"{assignedRole} account '{newUser.Username}' created successfully.");
        }

        public List<User> GetAllUsers() => _users;

        public string GenerateToken(User user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(double.Parse(jwtSettings["ExpireHours"] ?? "8")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}