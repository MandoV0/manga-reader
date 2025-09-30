using MangaReaderAPI.DTOs;
using MangaReaderAPI.Repositories;

namespace MangaReaderAPI.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly ISeriesRepository _repo;

        public SeriesService(ISeriesRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ChapterDto>?> GetChapters(int seriesId)
        {
            var series = await _repo.GetSeries(seriesId);
            return series?.Chapters?.Select(c => new ChapterDto
            {
                Id = c.Id,
                Title = c.Title,
                PageCount = c.Pages.Count,
                Pages = c.Pages
            });
        }

        public async Task<IEnumerable<SeriesListDto>> GetAllSeries(int page, int pageSize, string sort)
        {
            var series = await _repo.GetAllSeries(page, pageSize, sort);
            return series.Select(s => new SeriesListDto { Id = s.Id, Title = s.Title });
        }

        public async Task<IEnumerable<SeriesListDto>> GetPopular()
        {
            var series = await _repo.GetPopular();
            return series.Select(s => new SeriesListDto { Id = s.Id, Title = s.Title });
        }

        public async Task<IEnumerable<SeriesListDto>> GetRecentlyUpdated()
        {
            var series = await _repo.GetRecentlyUpdated();
            return series.Select(s => new SeriesListDto { Id = s.Id, Title = s.Title });
        }

        public async Task<SeriesDto?> GetSeries(int id)
        {
            var series = await _repo.GetSeries(id);
            if (series == null) return null;

            return new SeriesDto
            {
                Id = series.Id,
                Title = series.Title,
                Description = series.Description,
                Author = series.Author,
                Publisher = series.Publisher,
                Status = series.Status.ToString(),
                Genres = series.Genres.Select(g => g.Name).ToList(),
                ReleaseDate = series.ReleaseDate,
                AverageRating = series.AverageRating
            };
        }

        public async Task<IEnumerable<SeriesListDto>> GetTrending()
        {
            var series = await _repo.GetTrending();
            return series.Select(s => new SeriesListDto { Id = s.Id, Title = s.Title });
        }
    }
}
