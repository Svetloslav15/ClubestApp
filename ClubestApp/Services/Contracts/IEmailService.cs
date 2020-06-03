namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    
    public interface IEmailService
    {
        void SendEmail(User receiver, string mailDescription, string subject);
    }
}