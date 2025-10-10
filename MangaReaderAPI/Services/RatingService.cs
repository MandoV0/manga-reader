using MangaReaderAPI.DTOs;
using MangaReaderAPI.Models;
using MangaReaderAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MangaReaderAPI.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _repo;
        private readonly ISeriesRepository _seriesRepo;
        private readonly UserTrackingService _userTracking;

        public RatingService(IRatingRepository ratingRepo, ISeriesRepository seriesRepo, UserTrackingService userTrackingService)
        {
            _repo = ratingRepo;
            _userTracking = userTrackingService;
            _seriesRepo = seriesRepo;
        }

        public async Task<Rating> CreateRating(int seriesId, CreateRatingDto dto)
        {
            var userId = _userTracking.GetUserId();
            if (!userId.HasValue) throw new UnauthorizedAccessException("User ID not found in JWT Token. Token is invalid or missing.");

            var series = await _seriesRepo.GetSeries(seriesId);
            if (series == null) throw new KeyNotFoundException($"Series with id {seriesId} does not exist");

            var existingRating = await _repo.GetByUserAndSeries(seriesId, userId.Value);
            if (existingRating != null) throw new InvalidOperationException("User has already rated this series");

            var rating = new Rating
            {
                UserId = userId.Value,
                Stars = dto.Rating,
                SeriesId = seriesId,
            };

            var result = await _repo.Add(rating);
            return result;
        }

        public Task<Rating?> UpdateRating(int seriesId, CreateRatingDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRating(int seriesId)
        {
            throw new NotImplementedException();
        }
    }
}