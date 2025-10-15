using MangaReaderAPI.Models;

namespace MangaReaderAPI.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenres();
    }
}