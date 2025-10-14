using MangaReaderAPI.Data;
using MangaReaderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MangaReaderAPI.Repositories
{
    public class ForgotPasswordRepository : IForgotPasswordRepository
    {
        private readonly AppDbContext _context;

        public ForgotPasswordRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(UserForgotPassword forgotPassword)
        {
            _context.UserForgotPasswords.Add(forgotPassword);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(UserForgotPassword forgotPassword)
        {
            _context.UserForgotPasswords.Remove(forgotPassword);
            return _context.SaveChangesAsync();
        }

        public Task<UserForgotPassword?> GetByTokenAsync(string token)
        {
            return _context.UserForgotPasswords
                .FirstOrDefaultAsync(fp => fp.ResetToken == token && !fp.Used && fp.Expiry > DateTime.UtcNow);
        }

        public Task UpdateAsync(UserForgotPassword forgotPassword)
        {
            _context.UserForgotPasswords.Update(forgotPassword);
            return _context.SaveChangesAsync();
        }
    }
}