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
        public async Task<ActionResult<PagedResponseDto<SeriesListDto>>> GetAllSeries([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string sort = "")
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 20;
            
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
        public async Task<IActionResult> UpdateLastReadChapter([FromBody] UpdateLastReadChapterDto dto)
        {
            await _service.UpdateOrCreateLastReadChapter(dto.SeriesId, dto.LastReadChapterId);
            return Ok();
        }

        [HttpGet("last-read")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<LastReadChapterDto>>> GetLastReadChapters([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var lastReadChapters = await _service.GetLastReadChapters(limit, offset);
            return Ok(lastReadChapters);
        }

        [HttpGet("chapters/{chapterId}")]
        public async Task<ActionResult<ChapterDto>> GetChapterById(int chapterId)
        {
            var chapter = await _service.GetChapterById(chapterId);
            if (chapter == null) return NotFound();
            return Ok(chapter);
        }

        [HttpPost("{id}/view")]
        [Authorize]
        public async Task<IActionResult> TrackSeriesView(int id)
        {
            await _service.TrackSeriesView(id);
            return Ok();
        }

        [HttpDelete("reading-history")]
        [Authorize]
        public async Task<IActionResult> ClearReadingHistory()
        {
            await _service.ClearAllReadingHistory();
            return NoContent();
        }
    }
}
