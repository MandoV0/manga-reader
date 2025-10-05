namespace MangaReaderAPI.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public int SeriesId { get; set; }
        public Series Series { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int ViewsCount { get; set; }
        public List<Page> Pages { get; set; } = new();
    }
}