using MangaReaderAPI.DTOs;
using MangaReaderAPI.Models;

namespace MangaReaderAPI.Services
{
    public interface IRatingService
    {
        Task<Rating> CreateRating(int seriesId, CreateRatingDto dto);
        Task<Rating?> UpdateRating(int seriesId, CreateRatingDto dto);
        Task DeleteRating(int seriesId);
    }
}