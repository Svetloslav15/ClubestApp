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
        public async Task AddClubShouldReturnAddedClub()
        {
            this.Before();

            AddClubInputModel clubInputModel = new AddClubInputModel()
            {
                Name = "Programmer",
                Fee = 30,
                PriceType = "2",
                IsPublic = true,
                Description = "some information describing the club dfvcsdfvdvsevre",
                Town = "Sofia",
                Interests = new List<string>()
                 {
                     "software engineering",
                     "programming",
                     "informatics"
                 }
            };

            User user = new User()
            {
                FirstName = "Test"
            };
            await this.dbContext.AddAsync(user);
            await this.dbContext.SaveChangesAsync();

            Club club = await this.clubService.AddClub(clubInputModel, user.Id);
            Assert.NotNull(club);
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