namespace ClubestApp.Services
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using ClubestApp.Data.Models;
    using Microsoft.Extensions.Configuration;

    public class EmailService
    {
        private IConfiguration configuration;
        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendEmail(User receiver, string mailDescription, string subject)
        {
            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress(receiver.Email, receiver.FirstName + " " + receiver.LastName));

            // From
            mailMsg.From = new MailAddress("admin@novoselski.net", "Clubest");

            // Subject and multipart/alternative Body
            mailMsg.Subject = subject;
            string text = mailDescription;
            mailMsg.Body = text;

            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            string username = this.configuration.GetConnectionString("SendGridUsername");
            string password = this.configuration.GetConnectionString("SendGridPassword");
            NetworkCredential credentials = new NetworkCredential(username, password);
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }
    }
}