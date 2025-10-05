namespace MangaReaderAPI.DTOs
{
    public class PageDto
    {
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}