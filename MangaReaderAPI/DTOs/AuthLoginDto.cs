
using System.ComponentModel.DataAnnotations;

namespace MangaReaderAPI.DTOs
{
    public class AuthLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
