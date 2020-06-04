namespace ClubestApp.Tests.Services.UnitTests
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class PollServiceTests
    {
        private ApplicationDbContext dbContext;
        private PollService pollService;

        public void Before()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new ApplicationDbContext(options);
            this.pollService = new PollService(this.dbContext);
        }

        [Fact]
        public async Task CreatePollShouldAddToDbCorrectly()
        {
            this.Before();

            AddPollInputModel inputModel = new AddPollInputModel()
            {
                Content = "Who is your favourite language?",
                Options = "A~B~C",
                IsMultichoice = "false",
                ExpiredDate = new DateTime(2020, 07, 1),
                ClubId = "dassasdffs",
                Votes = new List<string>(),
                PollId = ""
            };

            Poll poll = await this.pollService.CreatePoll(inputModel, "test");

            Assert.NotNull(poll);
        }
    }
}