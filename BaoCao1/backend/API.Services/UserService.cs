using Microsoft.EntityFrameworkCore;
using TranAnhDung.API.DataAccess.Context;
using BCrypt.Net;
using TranAnhDung.API.Services.Interface;
using System.Threading.Tasks;
using Example07.Models;

namespace TranAnhDung.API.Services
{
    public class UserService : IUserService
    {
        private readonly EFDataContext _context;

        public UserService(EFDataContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}
