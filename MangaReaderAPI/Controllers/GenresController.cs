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
        private readonly IEmailService _emailService;

        public GenresController(IGenreService service, IEmailService emailService)
        {
            _service = service;
            _emailService = emailService;
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