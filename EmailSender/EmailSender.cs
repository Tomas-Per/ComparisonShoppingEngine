using System.Net;
using System.Net.Mail;

namespace EmailSender
{
    public class EmailSender
    {
        public void SendEmail(string subject, string htmlMessage, string recipient)
        {
            var smtpClient = new SmtpClient(EmailSettings.Default.host)
            {
                Port = EmailSettings.Default.port,
                Credentials = new NetworkCredential(EmailSettings.Default.email, EmailSettings.Default.password),
                EnableSsl = EmailSettings.Default.ssl,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(EmailSettings.Default.email),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(recipient);
            smtpClient.Send(mailMessage);

        }

    }
}
