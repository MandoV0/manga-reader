using System.Collections.Generic;

namespace MangaReaderAPI.Models
{
    public class Manga
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public List<Chapter> Chapters { get; set; } = new();
    }
}