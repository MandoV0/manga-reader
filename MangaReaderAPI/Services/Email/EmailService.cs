using SendGrid;
using SendGrid.Helpers.Mail;

namespace MangaReaderAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _sendGridApiKey;

        public EmailService(IConfiguration configuration)
        {
            _sendGridApiKey = configuration["SendGrid:ApiKey"]!;
            if (string.IsNullOrWhiteSpace(_sendGridApiKey))
            {
                throw new Exception("Missing the SendGrid API Key.");
            }
        }

        public async Task SendEmailAsync(string receiverEmail, string subject, string htmlContent)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("no-reply@mangareaderapi.com", "MangaReader");
            var to = new EmailAddress(receiverEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent);
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception("Failed to send email.");
            }
        }
    }

}