using MangaReaderAPI.DTOs;
using MangaReaderAPI.Models;

namespace MangaReaderAPI.Services
{
    public interface ISeriesService
    {
        SeriesDto? GetSeries(int id);
        IEnumerable<SeriesListDto> GetTrending();
        IEnumerable<SeriesListDto> GetPopular();
        IEnumerable<SeriesListDto> GetRecentlyUpdated();
        IEnumerable<ChapterDto>? GetChapters(int seriesId);
    }
}