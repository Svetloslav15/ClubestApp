namespace ClubestApp.Tests.Services.UnitTests
{
    using ClubestApp.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Xunit;
    using System;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Identity;
    using ClubestApp.Data.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserServiceTests : IClassFixture<DbFixture>
    {
        private ServiceProvider serviceProvider;
        private ApplicationDbContext db;
        private UserService service;

        public UserServiceTests(DbFixture dbFixture)
        {
            this.serviceProvider = dbFixture.ServiceProvider;
        }

        public UserService Before()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            this.db = new ApplicationDbContext(options);

            var service = new UserService(this.db, 
                    this.serviceProvider.GetService<SignInManager<User>>(),
                    this.serviceProvider.GetService<IConfiguration>()
                );

            return service;
        }

        [Fact]
        public async Task FindUserByIdShouldReturnCorrectUser()
        {
            this.service = this.Before();

            var testUser = new User();
            this.db.Users.Add(testUser);
            await this.db.SaveChangesAsync();
            var returnedUser = await service.FindUserById(testUser.Id);

            Assert.Equal(testUser.Id, returnedUser.Id);
        }
    }
}