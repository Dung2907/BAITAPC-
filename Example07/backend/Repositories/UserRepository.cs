using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Example07.Data;
using Example07.Models;

namespace Example07.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;

            _context.Database.Migrate();

            if (!_context.User.Any())
            {
                var user = new User
                {
                    Email = "trananhdung@gmail.com",
                    Password = _passwordHasher.HashPassword(null, "123456"),
                    RefreshToken = "example_refresh_token" // Ensure this is not null if column doesn't allow nulls
                };

                _context.Add(user);
                _context.SaveChanges();
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        }

        public async Task AddUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<string> GetRefreshTokenAsync(int userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                string refreshToken = user.RefreshToken ?? "defaultToken"; // Use a default value if null
                return refreshToken;
            }
            else
            {
                // Handle the case where the user is not found
                throw new Exception("User not found");
            }
        }
    }
}
