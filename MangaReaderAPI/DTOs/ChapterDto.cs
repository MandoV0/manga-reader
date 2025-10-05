using MangaReaderAPI.Models;

namespace MangaReaderAPI.DTOs
{
    public class ChapterDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<PageDto> Pages { get; set; } = new();
        public int PagesCount => Pages?.Count ?? 0;
        public int ViewsCount { get; set; }
    }
}