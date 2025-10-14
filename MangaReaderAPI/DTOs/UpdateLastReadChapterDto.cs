using System.ComponentModel.DataAnnotations;

namespace MangaReaderAPI.DTOs
{
    public class UpdateLastReadChapterDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "SeriesId must be a positive integer")]
        public int SeriesId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "LastReadChapterId must be a positive integer")]
        public int LastReadChapterId { get; set; }
    }
}
