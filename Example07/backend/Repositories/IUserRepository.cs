using System.Threading.Tasks;
using Example07.Models;

namespace Example07.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByRefreshTokenAsync(string refreshToken);
        Task AddUserAsync(User user);
        Task CommitAsync(); // Ensure this method is included
    }
}
