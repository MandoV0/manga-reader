using MangaReaderAPI.DTOs;
using MangaReaderAPI.Models;

namespace MangaReaderAPI.Services
{
    public interface IRatingService
    {
        Task<Rating?> CreateOrUpdateRating(int seriesId, CreateRatingDto dto);
        Task DeleteRating(int seriesId);
        Task<Rating?> GetUserRating(int seriesId);
    }
}