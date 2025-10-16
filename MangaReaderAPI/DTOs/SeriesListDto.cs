namespace MangaReaderAPI.DTOs
{
    public class SeriesListDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? BannerImageUrl { get; set; }
    }
}