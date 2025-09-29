using MangaReaderAPI.Models;

namespace MangaReaderAPI.Repositories
{
    public interface ISeriesRepository
    {
        Series? GetSeries(int id);
        IEnumerable<Series> GetAllSeries();
        IEnumerable<Series> GetTrending();
        IEnumerable<Series> GetPopular();
        IEnumerable<Series> GetRecentlyUpdated();
    }
}