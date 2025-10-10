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
            var rating = await _service.CreateRating(seriesId, dto);

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
            var updated = await _service.UpdateRating(seriesId, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
}