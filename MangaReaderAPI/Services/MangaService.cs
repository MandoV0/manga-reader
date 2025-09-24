using MangaReaderAPI.Models;
using MangaReaderAPI.Repositories;

namespace MangaReaderAPI.Services
{
    public class MangaService
    {
        private readonly IMangaRepository _repository;

        public MangaService(IMangaRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Manga>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Manga?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task AddAsync(Manga manga) => _repository.AddAsync(manga);
    }
}