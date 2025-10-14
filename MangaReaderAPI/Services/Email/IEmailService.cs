namespace MangaReaderAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string receiverEmail, string subject, string htmlContent);
    }
}