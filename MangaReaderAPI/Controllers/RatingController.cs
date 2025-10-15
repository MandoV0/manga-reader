using MangaReaderAPI.DTOs;
using MangaReaderAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaReaderAPI.Controllers
{
    [Route("api/v1/series/{seriesId}/rate")]
    [ApiController] /* With this attribute ModelState.IsValid runs automatically */
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _service;

        public RatingController(IRatingService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRating([FromRoute] int seriesId, [FromBody] CreateRatingDto dto)
        {
            var rating = await _service.CreateOrUpdateRating(seriesId, dto);

            return CreatedAtAction(
                nameof(SeriesController.GetSeries),
                "Series",
                new { id = seriesId },
                rating
            );
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateRating([FromRoute] int seriesId, [FromBody] CreateRatingDto dto)
        {
            var updated = await _service.CreateOrUpdateRating(seriesId, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserRating([FromRoute] int seriesId)
        {
            try
            {
                var rating = await _service.GetUserRating(seriesId);
                if (rating == null) return NotFound();
                return Ok(rating);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteRating([FromRoute] int seriesId)
        {
            try
            {
                await _service.DeleteRating(seriesId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}