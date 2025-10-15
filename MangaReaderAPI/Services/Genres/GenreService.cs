using MangaReaderAPI.Models;
using MangaReaderAPI.Repositories;

namespace MangaReaderAPI.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repo;

        public GenreService(IGenreRepository genreRepository)
        {
            _repo = genreRepository;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _repo.GetAllGenres();
        }
    }
}