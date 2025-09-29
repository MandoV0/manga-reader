namespace MangaReaderAPI.Models
{
    public class Rating
    {
        public int SeriesId { get; set; }
        public int UserId { get; set; }
        public int Stars { get; set; }
    }
}