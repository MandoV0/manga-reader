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
            var since = DateTime.UtcNow.AddHours(-24);

            var trendingSeries = await _context.SeriesViews
                .Where(v => v.ViewedAt >= since)            // Last 24 Hours
                .GroupBy(v => v.SeriesId)                   // Group by series
                .OrderByDescending(g => g.Count())          // Most viewed first
                .Select(g => g.Key)                         // get SeriesId
                .Take(10)                                   // Top 10
                .Join(_context.Series,
                    id => id,
                    s => s.Id,
                    (id, s) => s)
                .ToListAsync();

            return trendingSeries;
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

        public async Task<Chapter?> GetChapterById(int id)
        {
            return await _context.Chapters.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task Update(Series series)
        {
            _context.Series.Update(series);
            await _context.SaveChangesAsync();
        }

        public async Task<UserSeriesReadingHistory> CreateLastReadChapter(UserSeriesReadingHistory lastRead)
        {
            await _context.UserSeriesReadingHistories.AddAsync(lastRead);
            await _context.SaveChangesAsync();
            return lastRead;
        }

        public async Task<UserSeriesReadingHistory> UpdateLastReadChapter(UserSeriesReadingHistory lastRead)
        {
            _context.UserSeriesReadingHistories.Update(lastRead);
            await _context.SaveChangesAsync();
            return lastRead;
        }

        public async Task<List<UserSeriesReadingHistory>> GetLastReaderChapters(int userId, int limit=10, int offset=0)
        {
            return await _context.UserSeriesReadingHistories.Where(usr => usr.UserId == userId).OrderBy(usr => usr.Id).Skip(offset).Take(limit).ToListAsync();
        }
    }
}