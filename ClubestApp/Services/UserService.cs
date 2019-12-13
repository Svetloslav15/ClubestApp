namespace ClubestApp.Services
{
    using ClubestApp.Data;

    public class UserService
    {
        private ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}