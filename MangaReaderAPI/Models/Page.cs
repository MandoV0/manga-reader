namespace MangaReaderAPI.Models
{
    public class Page
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; } = null!;
        public int Index { get; set; } /* Page Order */
        public string ImageUrl { get; set; } = string.Empty;
    }
}