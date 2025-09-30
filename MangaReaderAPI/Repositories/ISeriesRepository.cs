using MangaReaderAPI.Models;

namespace MangaReaderAPI.Repositories
{
    public interface ISeriesRepository
    {
        Task<Series?> GetSeries(int id);
        Task<IEnumerable<Series>> GetAllSeries();
        Task<IEnumerable<Series>> GetTrending();
        Task<IEnumerable<Series>> GetPopular();
        Task<IEnumerable<Series>> GetRecentlyUpdated();
    }
}
