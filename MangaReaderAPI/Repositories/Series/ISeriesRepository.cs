using MangaReaderAPI.DTOs;
using MangaReaderAPI.Models;

namespace MangaReaderAPI.Repositories
{
    public interface ISeriesRepository
    {
        Task<Series?> GetSeries(int id);
        Task<IEnumerable<Series>> GetAllSeries(int page, int pageSize, string sort);
        Task<int> GetTotalSeriesCount();
        Task<IEnumerable<Series>> GetTrending();
        Task<IEnumerable<Series>> GetPopular();
        Task<IEnumerable<Series>> GetRecentlyUpdated();
        Task<Chapter?> GetChapterById(int chapterId);
        Task<SeriesView?> GetSeriesView(int seriesId, int userId);
        Task AddSeriesView(int seriesId, int userId);
        Task<UserSeriesReadingHistory> CreateLastReadChapter(UserSeriesReadingHistory lastRead);
        Task<UserSeriesReadingHistory> UpdateLastReadChapter(UserSeriesReadingHistory lastRead);
        Task<UserSeriesReadingHistory?> GetLastReadChapter(int userId, int seriesId);
        Task<List<UserSeriesReadingHistory>> GetLastReadChapters(int userId, int limit = 10, int offset = 0);
        Task ClearAllReadingHistory(int userId);
        Task SaveChangesAsync();
        Task Update(Series series);
    }
}
