﻿namespace ClubestApp.Services
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using ClubestApp.Data.Models;
    using Microsoft.Extensions.Configuration;

    public class EmailService
    {
        private IConfiguration configuration;
        private readonly string adminEmail = "admin@novoselski.net";
        private readonly string mailTitle = "Clubest";

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendEmail(User receiver, string mailDescription, string subject)
        {
            MailMessage mailMsg = new MailMessage();

            mailMsg.To.Add(new MailAddress(receiver.Email, receiver.FirstName + " " + receiver.LastName));
            mailMsg.From = new MailAddress(this.adminEmail, mailTitle);

            mailMsg.Subject = subject;
            string text = mailDescription;
            mailMsg.Body = text;

            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            string username = this.configuration.GetConnectionString("SendGridUsername");
            string password = this.configuration.GetConnectionString("SendGridPassword");
            NetworkCredential credentials = new NetworkCredential(username, password);
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }
    }
}