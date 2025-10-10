using MangaReaderAPI.Data;
using MangaReaderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MangaReaderAPI.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _context;

        public RatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Rating> Add(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<Rating?> Update(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating?> GetByUserAndSeries(int seriesId, int userId)
        {
            return await _context.Ratings.FirstOrDefaultAsync(r => r.SeriesId == seriesId && r.UserId == userId);
        }
    }
}