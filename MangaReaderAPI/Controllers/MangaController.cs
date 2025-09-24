using Microsoft.AspNetCore.Mvc;
using MangaReaderAPI.Models;
using MangaReaderAPI.Services;

namespace MangaReaderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangaController : ControllerBase
    {
        private readonly MangaService _service;

        public MangaController(MangaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mangas = await _service.GetAllAsync();
            return Ok(mangas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var manga = await _service.GetByIdAsync(id);
            if (manga == null) return NotFound();
            return Ok(manga);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Manga manga)
        {
            await _service.AddAsync(manga);
            return CreatedAtAction(nameof(GetById), new { id = manga.Id }, manga);
        }
    }
}