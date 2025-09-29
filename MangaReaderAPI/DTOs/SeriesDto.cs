namespace MangaReaderAPI.DTOs
{
    public class SeriesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Status { get; set; }
        public List<string> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double AverageRating { get; set; }
    }   
}