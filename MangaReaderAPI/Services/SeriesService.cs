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

        public IEnumerable<ChapterDto>? GetChapters(int seriesId)
        {
            return _repo.GetSeries(seriesId)?.Chapters?.Select(c => new ChapterDto
            {
                Id = c.Id,
                Title = c.Title,
                PageCount = c.Pages.Count,
                Pages = c.Pages
            });
        }

        public IEnumerable<SeriesListDto> GetPopular()
        {
            return _repo.GetPopular().Select(s => new SeriesListDto { Id = s.Id, Title = s.Title });
        }

        public IEnumerable<SeriesListDto> GetRecentlyUpdated()
        {
            return _repo.GetRecentlyUpdated().Select(s => new SeriesListDto { Id = s.Id, Title = s.Title });
        }

        public SeriesDto? GetSeries(int id)
        {
            var series = _repo.GetSeries(id);
            if (series == null) return null;

            return new SeriesDto
            {
                Id = series.Id,
                Title = series.Title,
                Description = series.Description,
                Author = series.Author,
                Publisher = series.Publisher,
                Status = series.Status,
                Genres = series.Genres,
                ReleaseDate = series.ReleaseDate,
                AverageRating = series.AverageRating
            };
        }

        public IEnumerable<SeriesListDto> GetTrending()
        {
            return _repo.GetTrending().Select(s => new SeriesListDto { Id = s.Id, Title = s.Title });
        }
    }
}