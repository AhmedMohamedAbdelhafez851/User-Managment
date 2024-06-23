using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class EmailSender : IEmailSender
    {
        private static readonly string _fromMail = "ahmedmhafez851@gmail.com";
        private static readonly string _fromPassword = "Durghmee11@";
        private static readonly string _smtpServer = "smtp.gmail.com";
        private static readonly int _smtpPort = 587;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress(_fromMail);
                message.Subject = subject;
                message.To.Add(email);
                message.Body = htmlMessage;
                message.IsBodyHtml = true;

                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_fromMail, _fromPassword);
                    smtpClient.EnableSsl = true; // Ensure SSL/TLS is enabled

                    await smtpClient.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }
    }
}
