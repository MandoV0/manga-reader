using MangaReaderAPI.Models;

namespace MangaReaderAPI.Repositories
{
    public interface IForgotPasswordRepository
    {
        Task AddAsync(UserForgotPassword forgotPassword);
        Task<UserForgotPassword?> GetByTokenAsync(string token);
        Task UpdateAsync(UserForgotPassword forgotPassword);
        Task DeleteAsync(UserForgotPassword forgotPassword);
    }
}