namespace MangaReaderAPI.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string email, IEnumerable<string> roles = null);
    }
}