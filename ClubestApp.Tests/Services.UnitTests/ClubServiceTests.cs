namespace ClubestApp.Tests.Services.UnitTests
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using ClubestApp.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class ClubServiceTests : IClassFixture<DbFixture>
    {
        private ServiceProvider serviceProvider;
        private ApplicationDbContext dbContext;
        private ClubService clubService;
        private Mock<IUserService> userServiceMock;
        private Mock<ICloudinaryService> cloudinaryServiceMock;

        public ClubServiceTests(DbFixture dbFixture)
        {
            this.serviceProvider = dbFixture.ServiceProvider;
            this.userServiceMock = new Mock<IUserService>();
            this.cloudinaryServiceMock = new Mock<ICloudinaryService>();
        }

        private void Before()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new ApplicationDbContext(options);

            this.clubService = new ClubService(this.dbContext,
                this.userServiceMock.Object, 
                this.cloudinaryServiceMock.Object, 
                this.serviceProvider.GetService<UserManager<User>>());
        }

        [Fact]
        public async Task AddRequestNewClubShouldAddRequest()
        {
            this.Before();

            AddClubInputModel inputModel = new AddClubInputModel()
            {
                Name = "test",
                Fee = 30,
                IsPublic = true,
                PriceType = "2",
                Description = "fsmdklvjdfvbjdfkvbnjkdbnvjkefrnvejknvbioenvdfrovgejv",
                Town = "Sofia",
                Interests = new List<string>()
                {
                    "sdfscfs", "sdvs", "sdcscs"
                }
            };

            RequestNewClub request = await this.clubService.AddRequestNewClub(inputModel, null);

            Assert.NotNull(request);
        }
    }
}