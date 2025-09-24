namespace MangaReaderAPI.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public string Title { get; set; } = null!;
        public int Number { get; set; }

        public Manga Manga { get; set; } = null!;
    }
}