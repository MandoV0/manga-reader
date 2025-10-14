using MangaReaderAPI.DTOs;
using MangaReaderAPI.Models;

namespace MangaReaderAPI.Services
{
    public interface ISeriesService
    {
        Task<SeriesDto?> GetSeries(int id);
        Task<IEnumerable<ChapterDto>?> GetChapters(int seriesId);
        Task<ChapterDto?> GetChapterById(int chapterId);
        Task<IEnumerable<SeriesListDto>> GetTrending();
        Task<IEnumerable<SeriesListDto>> GetPopular();
        Task<IEnumerable<SeriesListDto>> GetRecentlyUpdated();
        Task<PagedResponseDto<SeriesListDto>> GetAllSeries(int page, int pageSize, string sort);
        Task UpdateOrCreateLastReadChapter(int seriesId, int lastReadChapterId);
        Task<IEnumerable<LastReadChapterDto>> GetLastReadChapters(int limit = 10, int offset = 0);
    }
}
