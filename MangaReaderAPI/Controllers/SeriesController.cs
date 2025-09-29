using Microsoft.AspNetCore.Mvc;
using MangaReaderAPI.Models;
using MangaReaderAPI.Services;
using MangaReaderAPI.DTOs;

namespace MangaReaderAPI.Controllers
{
    [Route("api/[controller]")]
    public class SeriesController : ControllerBase
    {
        private readonly SeriesService _service;

        public SeriesController(SeriesService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<SeriesDto> GetSeries(int id)
        {
            var series = _service.GetSeries(id);
            if (series == null) return NotFound();
            return Ok(series);
        }

        [HttpGet("{id}/chapters")]
        public ActionResult<IEnumerable<ChapterDto>> GetChapters(int id)
        {
            var chapters = _service.GetChapters(id);
            if (chapters == null) return NotFound();
            return Ok(chapters);
        }

        [HttpGet("trending")]
        public ActionResult<IEnumerable<SeriesListDto>> GetTrending()
        {
            var series = _service.GetTrending();
            return Ok(series);
        }

        [HttpGet("popular")]
        public ActionResult<IEnumerable<SeriesListDto>> GetPopular()
        {
            var series = _service.GetPopular();
            return Ok(series);
        }

        [HttpGet("recently-updated")]
        public ActionResult<IEnumerable<SeriesListDto>> GetRecentlyUpdated()
        {
            var series = _service.GetRecentlyUpdated();
            return Ok(series);
        }

    }
}