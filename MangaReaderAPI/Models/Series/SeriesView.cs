namespace MangaReaderAPI.Models
{
    public class SeriesView
    {
        public int Id { get; set; }

        public int SeriesId { get; set; }
        public Series Series { get; set; } = null!;

        /* Nullable in case we have a User that is not logged in, so doesnt provice a Bearer Token to identify him */
        public int? UserId { get; set; }
        public User? User { get; set; } = null!;

        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
    }
}