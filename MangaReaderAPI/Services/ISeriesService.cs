using MangaReaderAPI.DTOs;

namespace MangaReaderAPI.Services
{
    public interface ISeriesService
    {
        Task<SeriesDto?> GetSeries(int id);
        Task<IEnumerable<ChapterDto>?> GetChapters(int seriesId);
        Task<IEnumerable<SeriesListDto>> GetTrending();
        Task<IEnumerable<SeriesListDto>> GetPopular();
        Task<IEnumerable<SeriesListDto>> GetRecentlyUpdated();
        Task<IEnumerable<SeriesListDto>> GetAllSeries(int page, int pageSize, string sort);
    }
}
