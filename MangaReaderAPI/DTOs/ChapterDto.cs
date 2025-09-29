namespace MangaReaderAPI.DTOs
{
    public class ChapterDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public List<string> Pages { get; set; }
    }
}