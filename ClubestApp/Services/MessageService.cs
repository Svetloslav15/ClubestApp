namespace ClubestApp.Services
{
    using ClubestApp.Data;

    public class MessageService
    {
        private readonly ApplicationDbContext dbContext;

        public MessageService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
