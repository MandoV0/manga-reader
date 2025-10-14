
using System.ComponentModel.DataAnnotations;

namespace MangaReaderAPI.DTOs
{
    public class AuthRegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = String.Empty;
    }
}
