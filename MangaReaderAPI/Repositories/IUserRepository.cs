using MangaReaderAPI.DTOs;
using MangaReaderAPI.Models;

namespace MangaReaderAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
    }
}