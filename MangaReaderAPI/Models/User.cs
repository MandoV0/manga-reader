using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MangaReaderAPI.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string PasswordHash { get; set; } = String.Empty;

        [Required]
        public string Salt { get; set; } = String.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserSeriesReadingHistory> ReadingHistories { get; set; } = new List<UserSeriesReadingHistory>();
    }
}