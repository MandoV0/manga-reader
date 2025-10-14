using Microsoft.AspNetCore.Mvc;
using MangaReaderAPI.Services;
using MangaReaderAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MangaReaderAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesService _service;

        public SeriesController(ISeriesService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeriesDto>> GetSeries(int id)
        {
            var series = await _service.GetSeries(id);
            if (series == null) return NotFound();
            return Ok(series);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeriesListDto>>> GetAllSeries([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string sort = "")
        {
            var series = await _service.GetAllSeries(page, pageSize, sort);
            return Ok(series);
        }

        [HttpGet("{id}/chapters")]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> GetChapters(int id)
        {
            var chapters = await _service.GetChapters(id);
            if (chapters == null) return NotFound();
            return Ok(chapters);
        }

        [HttpGet("trending")]
        public async Task<ActionResult<IEnumerable<SeriesListDto>>> GetTrending()
        {
            var series = await _service.GetTrending();
            return Ok(series);
        }

        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<SeriesListDto>>> GetPopular()
        {
            var series = await _service.GetPopular();
            return Ok(series);
        }

        [HttpGet("recently-updated")]
        public async Task<ActionResult<IEnumerable<SeriesListDto>>> GetRecentlyUpdated()
        {
            var series = await _service.GetRecentlyUpdated();
            return Ok(series);
        }

        [HttpPost("last-read")]
        [Authorize]
        public async Task<IActionResult> UpdateLastReadChapter(int seriesId, int lastReadChapterId)
        {
            await _service.UpdateOrCreateLastReadChapter(seriesId, lastReadChapterId);
            return Ok();
        }

    }
}
