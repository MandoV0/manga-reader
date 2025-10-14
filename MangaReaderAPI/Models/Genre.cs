using System.Collections.Generic;

namespace MangaReaderAPI.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public ICollection<Series> Series { get; set; } = new List<Series>();
    }
}
