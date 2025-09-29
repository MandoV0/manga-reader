using MangaReaderAPI.Models;

namespace MangaReaderAPI.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Status { get; set; } // "Ongoing", "Completed", "Cancelled"
        public List<string> Genres { get; set; } = new();
        public DateTime ReleaseDate { get; set; }
        public double AverageRating { get; set; }
        public List<Chapter> Chapters { get; set; } = new();
    }
}