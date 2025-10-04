
using MangaReaderAPI.Models;
using System.Threading.Tasks;

namespace MangaReaderAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int userId);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<User> CreateUser(User user);
    }
}
