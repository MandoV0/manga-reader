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
    }
}
