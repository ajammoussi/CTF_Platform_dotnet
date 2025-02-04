using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
namespace CTF_Platform_dotnet.Services.EmailSender
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridClient _client;
        private readonly EmailAddress _fromAddress;
        public SendGridEmailSender(IConfiguration config)
        {
            // Fixed: Use correct configuration keys and include FromName
            _client = new SendGridClient(config["SendGrid:ApiKey"]);
            _fromAddress = new EmailAddress(
                config["SendGrid:FromEmail"],         // Email address
                config["SendGrid:FromName"]           // Display name
            );
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var msg = new SendGridMessage
            {
                From = _fromAddress,  // Use the pre-configured EmailAddress object
                Subject = subject,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));

            Console.WriteLine($"Sending email to {email} with subject '{subject}'");

            var response = await _client.SendEmailAsync(msg);

            Console.WriteLine($"Response Status Code: {(int)response.StatusCode}");
            Console.WriteLine($"Response Body: {await response.Body.ReadAsStringAsync()}");

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Body.ReadAsStringAsync();
                throw new Exception($"Email sending failed ({(int)response.StatusCode}): {errorBody}");
            }
            else
            {
                Console.WriteLine("Email sent successfully.");
            }
        }
    }
}