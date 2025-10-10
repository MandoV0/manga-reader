using MangaReaderAPI.Models;

namespace MangaReaderAPI.Repositories
{
    public interface IRatingRepository
    {
        Task<Rating> Add(Rating rating);
        Task<Rating?> Update(Rating rating);
        Task Delete();
        Task<Rating?> GetByUserAndSeries(int seriesId, int userId);
    }
}