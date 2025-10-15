using MangaReaderAPI.Data;
using MangaReaderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MangaReaderAPI.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}