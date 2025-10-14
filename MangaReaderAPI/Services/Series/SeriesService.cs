using MangaReaderAPI.DTOs;
using MangaReaderAPI.Models;
using MangaReaderAPI.Repositories;

namespace MangaReaderAPI.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly ISeriesRepository _repo;
        private readonly IUserTrackingService _userTracking;

        public SeriesService(ISeriesRepository repo, IUserTrackingService userTrackingService)
        {
            _repo = repo;
            _userTracking = userTrackingService;
        }

        public async Task<IEnumerable<ChapterDto>?> GetChapters(int seriesId)
        {
            var series = await _repo.GetSeries(seriesId);
            return series?.Chapters?.Select(c => new ChapterDto
            {
                Id = c.Id,
                Title = c.Title,
                Pages = c.Pages.Select(p => new PageDto
                {
                    Id = p.Id,
                    PageNumber = p.Index,
                    ImageUrl = p.ImageUrl
                }).ToList()
            });
        }

        public async Task<PagedResponseDto<SeriesListDto>> GetAllSeries(int page, int pageSize, string sort)
        {
            var series = await _repo.GetAllSeries(page, pageSize, sort);
            var totalCount = await _repo.GetTotalSeriesCount();
            
            var seriesDtos = series.Select(s => new SeriesListDto { Id = s.Id, Title = s.Title, CoverImageUrl = s.CoverImageUrl });
            
            return new PagedResponseDto<SeriesListDto>
            {
                Items = seriesDtos,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<IEnumerable<SeriesListDto>> GetPopular()
        {
            var series = await _repo.GetPopular();
            return series.Select(s => new SeriesListDto { Id = s.Id, Title = s.Title, CoverImageUrl = s.CoverImageUrl });
        }

        public async Task<IEnumerable<SeriesListDto>> GetRecentlyUpdated()
        {
            var series = await _repo.GetRecentlyUpdated();
            return series.Select(s => new SeriesListDto { Id = s.Id, Title = s.Title, CoverImageUrl = s.CoverImageUrl });
        }

        public async Task<SeriesDto?> GetSeries(int id)
        {
            var series = await _repo.GetSeries(id);
            if (series == null) return null;

            var userId = _userTracking.GetUserId();

            if (userId.HasValue)
            {
                var existingView = await _repo.GetSeriesView(series.Id, userId.Value);

                if (existingView == null)
                {
                    await _repo.AddSeriesView(series.Id, userId.Value);
                    series.ViewsCount += 1;
                }
                else
                {
                    existingView.ViewedAt = DateTime.UtcNow;
                }

                await _repo.SaveChangesAsync();
            }

            return new SeriesDto
            {
                Id = series.Id,
                Title = series.Title,
                Description = series.Description,
                Author = series.Author,
                CoverImageUrl = series.CoverImageUrl,
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
            return series.Select(s => new SeriesListDto { Id = s.Id, Title = s.Title, CoverImageUrl = s.CoverImageUrl });
        }

        public async Task UpdateOrCreateLastReadChapter(int seriesId, int lastReadChapterId)
        {
            var userId = _userTracking.GetUserId();
            if (!userId.HasValue) return;

            var series = await _repo.GetSeries(seriesId);
            if (series == null) return;

            var chapter = await _repo.GetChapterById(lastReadChapterId);
            if (chapter == null) return;

            var lastReadChapter = await _repo.GetLastReadChapter(userId.Value, series.Id);

            if (lastReadChapter != null)
            {
                lastReadChapter.LastReadChapterId = lastReadChapterId;
                await _repo.UpdateLastReadChapter(lastReadChapter);
            }
            else
            {
                var lastRead = new UserSeriesReadingHistory
                {
                    SeriesId = seriesId,
                    UserId = userId.Value,
                    LastReadChapterId = lastReadChapterId
                };
                await _repo.CreateLastReadChapter(lastRead);
            }
        }

        public async Task<IEnumerable<LastReadChapterDto>> GetLastReadChapters(int limit = 10, int offset = 0)
        {
            var userId = _userTracking.GetUserId();
            if (!userId.HasValue) return Enumerable.Empty<LastReadChapterDto>();

            var lastReadChapters = await _repo.GetLastReadChapters(userId.Value, limit, offset);
            var lastReadChapterDtos = new List<LastReadChapterDto>();

            foreach (var lastReadChapter in lastReadChapters)
            {
                var series = await _repo.GetSeries(lastReadChapter.SeriesId);
                var chapter = await _repo.GetChapterById(lastReadChapter.LastReadChapterId);

                if (series != null && chapter != null)
                {
                    lastReadChapterDtos.Add(new LastReadChapterDto
                    {
                        SeriesId = series.Id,
                        ChapterId = chapter.Id,
                        SeriesTitle = series.Title,
                        ChapterTitle = chapter.Title,
                        CoverImageUrl = series.CoverImageUrl ?? string.Empty
                    });
                }
            }

            return lastReadChapterDtos;
        }

        public async Task<ChapterDto?> GetChapterById(int chapterId)
        {
            var chapter = await _repo.GetChapterById(chapterId);
            if (chapter == null) return null;
            var dto = new ChapterDto
            {
                Id = chapter.Id,
                Title = chapter.Title,
                Pages = chapter.Pages.Select(p => new PageDto
                {
                    Id = p.Id,
                    PageNumber = p.Index,
                    ImageUrl = p.ImageUrl
                }).ToList()
            };
            return dto;
        }
    }
}