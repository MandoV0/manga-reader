
using System.ComponentModel.DataAnnotations;

namespace MangaReaderAPI.DTOs
{
    public class AuthLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
