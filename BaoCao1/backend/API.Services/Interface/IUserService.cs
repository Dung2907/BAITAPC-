using System.Threading.Tasks;
using Example07.Models;
using TranAnhDung.API.DataAccess.Entity;

namespace TranAnhDung.API.Services.Interface
{
    public interface IUserService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
