using MangaReaderAPI.Data;
using MangaReaderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MangaReaderAPI.Repositories
{
    public class MangaRepository : IMangaRepository
    {
        private readonly AppDbContext _context;

        public MangaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Manga>> GetAllAsync() =>
            await _context.Mangas.Include(m => m.Chapters).ToListAsync();

        public async Task<Manga?> GetByIdAsync(int id) =>
            await _context.Mangas.Include(m => m.Chapters)
                                 .FirstOrDefaultAsync(m => m.Id == id);

        public async Task AddAsync(Manga manga)
        {
            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();
        }
    }
}