
using System.ComponentModel.DataAnnotations;

namespace MangaReaderAPI.DTOs
{
    public class AuthDeleteDto
    {
        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
