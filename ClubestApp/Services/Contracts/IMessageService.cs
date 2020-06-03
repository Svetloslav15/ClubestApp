namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task<Message> AddMessage(string content, string clubId, User user);
    }
}