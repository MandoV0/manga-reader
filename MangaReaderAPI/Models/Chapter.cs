namespace MangaReaderAPI.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<string> Pages { get; set; } = new();
    }
}