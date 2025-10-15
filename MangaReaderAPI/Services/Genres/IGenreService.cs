using MangaReaderAPI.Models;

namespace MangaReaderAPI.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenres();
    }
}