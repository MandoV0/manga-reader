namespace MangaReaderAPI.Models
{
    public class UserSeriesReadingHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SeriesId { get; set; }
        public int LastReadChapterId { get; set; }
    }
}