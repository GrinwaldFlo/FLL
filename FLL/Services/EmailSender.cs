using FLL.Data.Models;
using FLL.Utils;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FLL.Services
{
    public class SmtpSenderOptions
    {
        public string? SmtpUser { get; set; }
        public string? SmtpPassword { get; set; }

        public string? SmtpUrl { get; set; }
    }

    public class EmailSender
    {
        private readonly IServiceProvider serviceProvider;

        public SmtpSenderOptions Options { get; }

        public EmailSender(IOptions<SmtpSenderOptions> optionsAccessor, IServiceProvider serviceProvider)
        {
            Options = optionsAccessor.Value;
            this.serviceProvider = serviceProvider;
        }

        public ApplicationDbContext? Db
        {
            get
            {
                try
                {
                    return serviceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();
                }
                catch (Exception ex)   // If there is an issue, takes the last available
                {
                    Serilog.Log.Error(ex, "Email, get ApplicationDbContext");
                }
                return null;
            }
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(subject, htmlMessage, email, email, "");
        }

        public Task<string> Execute(string subject, string message, string emailName, string email, string website)
        {
            try
            {
                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress(website, Options.SmtpUser));
                mailMessage.To.Add(new MailboxAddress(emailName, email));
                mailMessage.Subject = website + " - " + subject;

                BodyBuilder builder = new()
                {
                    HtmlBody = message,
                    TextBody = HtmlUtils.ConvertHtml(message)
                };
                mailMessage.Body = builder.ToMessageBody();

                using var smtpClient = new SmtpClient();
                smtpClient.Connect(Options.SmtpUrl, 587, SecureSocketOptions.StartTls);
                smtpClient.Authenticate(Options.SmtpUser, Options.SmtpPassword);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Send email");
                return Task.FromResult($"ERROR: {ex.Message}");
            }

            return Task.FromResult("MAIL SENT");
        }
    }
}