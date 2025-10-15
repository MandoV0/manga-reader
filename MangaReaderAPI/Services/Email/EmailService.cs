using SendGrid;
using SendGrid.Helpers.Mail;

namespace MangaReaderAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _sendGridApiKey;
        private readonly string _senderEmail;

        public EmailService(IConfiguration configuration)
        {
            _sendGridApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY")
                ?? throw new Exception("Missing SENDGRID_API_KEY in .env file.");
            _senderEmail = Environment.GetEnvironmentVariable("SENDGRID_SENDER_EMAIL")
                ?? throw new Exception("Missing SENDGRID_SENDER_EMAIL in .env file.");
        }

        public async Task SendEmailAsync(string receiverEmail, string subject, string htmlContent)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress(_senderEmail, "MangaReader");
            var to = new EmailAddress(receiverEmail);
            
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent);
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. {response.StatusCode}");
            }
        }
    }

}