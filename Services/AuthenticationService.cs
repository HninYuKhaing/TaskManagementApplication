using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Services
{
    public class AuthenticationService
    {
        private readonly TaskContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(TaskContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterUserAsync(string username, string email, string password, string firstName, string lastName, int roleId)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == username || u.Email == email))
                return false;

            var user = new User
            {
                UserName = username,
                Email = email,
                PasswordHash = HashPassword(password),
                FirstName = firstName,
                LastName = lastName,
                RoleId = roleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserName == username && u.PasswordHash == HashPassword(password));
            if (user != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id.ToString()),
                new Claim("FullName", user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

                var identity = new ClaimsIdentity(claims, "CustomAuth");
                var principal = new ClaimsPrincipal(identity);
                await _httpContextAccessor.HttpContext.SignInAsync("CustomAuthScheme", principal);

                return user;
            }
            return  null;
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync("CustomAuthScheme");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
