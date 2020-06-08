namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class PasswordTokenService
    {
        private readonly ApplicationDbContext dbContext;

        public PasswordTokenService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PasswordToken> GeneratePasswordToken(User user)
        {
            PasswordToken passwordToken = new PasswordToken()
            {
                User = user,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
            };

            await this.dbContext.PasswordTokens.AddAsync(passwordToken);
            await this.dbContext.SaveChangesAsync();

            return passwordToken;
        }

        public async Task<PasswordToken> FindById(string id)
        {
            return await this.dbContext.PasswordTokens
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}