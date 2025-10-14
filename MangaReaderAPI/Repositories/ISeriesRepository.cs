using MangaReaderAPI.Models;

namespace MangaReaderAPI.Repositories
{
    public interface ISeriesRepository
    {
        Task<Series?> GetSeries(int id);
        Task<IEnumerable<Series>> GetAllSeries(int page, int pageSize, string sort);
        Task<IEnumerable<Series>> GetTrending();
        Task<IEnumerable<Series>> GetPopular();
        Task<IEnumerable<Series>> GetRecentlyUpdated();
        Task<Chapter?> GetChapterById(int chaptetId);
        Task<SeriesView?> GetSeriesView(int seriesId, int userId);
        Task AddSeriesView(int seriesId, int userId);
        Task<UserSeriesReadingHistory> CreateLastReadChapter(UserSeriesReadingHistory lastRead);
        Task<UserSeriesReadingHistory> UpdateLastReadChapter(UserSeriesReadingHistory lastRead);
        Task SaveChangesAsync();
        Task Update(Series series);
    }
}
