using MangaReaderAPI.Models;

namespace MangaReaderAPI.Repositories
{
    public interface IMangaRepository
    {
        Task<List<Manga>> GetAllAsync();
        Task<Manga?> GetByIdAsync(int id);
        Task AddAsync(Manga manga);
    }
}
