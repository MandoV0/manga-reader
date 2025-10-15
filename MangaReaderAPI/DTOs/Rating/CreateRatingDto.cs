using System.ComponentModel.DataAnnotations;

namespace MangaReaderAPI.DTOs
{
    public class CreateRatingDto
    {
        [Required]
        [Range(0, 10)]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}