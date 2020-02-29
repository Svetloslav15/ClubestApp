namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using System;
    using System.Threading.Tasks;

    public class MessageService
    {
        private readonly ApplicationDbContext dbContext;

        public MessageService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Message> AddMessage(string content, string clubId, User user)
        {
            Message message = new Message()
            {
                Content = content,
                ClubId = clubId,
                SenderId = user.Id,
                Sender = user,
                Date = DateTime.UtcNow
            };

            var result = await this.dbContext.Messages.AddAsync(message);
            await this.dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
