using MangaReaderAPI.Data;
using MangaReaderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MangaReaderAPI.Repositories
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly AppDbContext _context;

        public SeriesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Series?> GetSeries(int id)
        {
            return await _context.Series
                .Include(s => s.Genres)
                .Include(s => s.Chapters)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Series>> GetAllSeries()
        {
            return await _context.Series.ToListAsync();
        }

        public async Task<IEnumerable<Series>> GetTrending()
        {
            return await _context.Series.OrderByDescending(s => s.AverageRating).Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Series>> GetPopular()
        {
            return await _context.Series.OrderByDescending(s => s.Chapters.Count).Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Series>> GetRecentlyUpdated()
        {
            return await _context.Series.OrderByDescending(s => s.ReleaseDate).Take(10).ToListAsync();
        }
    }
}
