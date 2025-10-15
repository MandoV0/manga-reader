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
        private readonly IUserTrackingService _userTracking;

        public RatingService(IRatingRepository ratingRepo, ISeriesRepository seriesRepo, IUserTrackingService userTrackingService)
        {
            _repo = ratingRepo;
            _userTracking = userTrackingService;
            _seriesRepo = seriesRepo;
        }

        public async Task<Rating?> CreateOrUpdateRating(int seriesId, CreateRatingDto dto)
        {
            var userId = _userTracking.GetUserId();
            if (!userId.HasValue)
                throw new UnauthorizedAccessException("User ID not found in JWT Token.");

            var series = await _seriesRepo.GetSeries(seriesId);
            if (series == null)
                throw new KeyNotFoundException($"Series with id {seriesId} does not exist.");

            var existingRating = await _repo.GetByUserAndSeries(seriesId, userId.Value);

            if (existingRating == null) // Create
            {
                var newRating = new Rating
                {
                    UserId = userId.Value,
                    SeriesId = seriesId,
                    Stars = dto.Rating
                };

                series.TotalRatingSum += dto.Rating;
                series.TotalRatings++;

                await _repo.Add(newRating);
            }
            else // Update
            {
                series.TotalRatingSum -= existingRating.Stars;
                series.TotalRatingSum += dto.Rating;
                existingRating.Stars = dto.Rating;

                await _repo.Update(existingRating);
            }
            
            series.AverageRating = (double) series.TotalRatingSum / series.TotalRatings;
            await _seriesRepo.Update(series);

            return existingRating;
        }

        public async Task DeleteRating(int seriesId)
        {
            var userId = _userTracking.GetUserId();
            if (!userId.HasValue)
                throw new UnauthorizedAccessException("User ID not found in JWT Token. Token is invalid or missing.");

            var series = await _seriesRepo.GetSeries(seriesId);
            if (series == null)
                throw new KeyNotFoundException($"Series with id {seriesId} does not exist");

            var existingRating = await _repo.GetByUserAndSeries(seriesId, userId.Value);
            if (existingRating == null)
                throw new KeyNotFoundException($"Rating for series {seriesId} by this user does not exist");

            series.TotalRatings--;
            series.TotalRatingSum -= existingRating.Stars;
            // To make sure we dont divide by zero when we delete the last rating
            series.AverageRating = series.TotalRatings > 0 ? (double) series.TotalRatingSum / series.TotalRatings : 0;

            await _seriesRepo.Update(series);
            await _repo.Delete(existingRating.Id);
        }

        public async Task<Rating?> GetUserRating(int seriesId)
        {
            var userId = _userTracking.GetUserId();
            if (!userId.HasValue)
                throw new UnauthorizedAccessException("User ID not found in JWT Token.");

            return await _repo.GetByUserAndSeries(seriesId, userId.Value);
        }
    }
}