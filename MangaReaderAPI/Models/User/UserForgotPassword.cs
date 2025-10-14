namespace MangaReaderAPI.Models
{
    public class UserForgotPassword
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ResetToken { get; set; } = null!;
        public DateTime Expiry { get; set; }
        public bool Used { get; set; } = false;
    }
}