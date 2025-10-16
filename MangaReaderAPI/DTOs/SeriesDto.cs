namespace MangaReaderAPI.DTOs
{
    public class SeriesDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string? CoverImageUrl { get; set; } = String.Empty;
        public string? BannerImageUrl { get; set; } = String.Empty;
        public string Author { get; set; } = String.Empty;
        public string Publisher { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;
        public List<string> Genres { get; set; } = new List<string>();
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        public double AverageRating { get; set; }
    }   
}