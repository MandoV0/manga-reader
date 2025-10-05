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
                .ThenInclude(c => c.Pages)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Series>> GetAllSeries(int page, int pageSize, string sort)
        {
            return await _context.Series.OrderBy(s => s.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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

        public async Task<SeriesView?> GetSeriesView(int seriesId, int userId)
        {
            return await _context.SeriesViews.FirstOrDefaultAsync(v => v.SeriesId == seriesId && v.UserId == userId);
        }

        public async Task AddSeriesView(int seriesId, int userId)
        {
            var view = new SeriesView { SeriesId = seriesId, UserId = userId };
            await _context.SeriesViews.AddAsync(view);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
