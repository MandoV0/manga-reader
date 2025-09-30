using MangaReaderAPI.Models;

using MangaReaderAPI.Models.Enums;

namespace MangaReaderAPI.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public SeriesStatus Status { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double AverageRating { get; set; }
        public List<Chapter> Chapters { get; set; } = new();
    }
}