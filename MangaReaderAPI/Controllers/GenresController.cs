using MangaReaderAPI.Models;
using MangaReaderAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaReaderAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
        {
            var genres = await _service.GetAllGenres();
            return Ok(genres);
        }
    }
}