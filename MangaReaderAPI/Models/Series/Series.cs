using MangaReaderAPI.Models;

using MangaReaderAPI.Models.Enums;

namespace MangaReaderAPI.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Author { get; set; } = String.Empty;
        public string Publisher { get; set; } = String.Empty;
        public int ViewsCount { get; set; }
        public SeriesStatus Status { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public string? CoverImageUrl { get; set; }
        public string? BannerImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;

        /* We use this so we dont have to do a full aggregation query every time a user Creates a review */ 
        public int TotalRatings { get; set; }
        public int TotalRatingSum { get; set; }

        public double AverageRating { get; set; }
        public List<Chapter> Chapters { get; set; } = new();
    }
}