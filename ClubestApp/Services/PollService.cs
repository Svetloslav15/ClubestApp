using ClubestApp.Data;
using ClubestApp.Data.Models;
using ClubestApp.Models.BindingModels;
using ClubestApp.Models.InputModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubestApp.Services
{
    public class PollService
    {
        private readonly ApplicationDbContext dbContext;

        public PollService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Poll> AddVote(List<string> votes, string pollId, string userId)
        {
            User user = await this.dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            Poll currentPoll = null;

            foreach (var vote in votes)
            {
                Option option = await this.dbContext
                    .Options
                    .FirstOrDefaultAsync(x => x.Content == vote && x.PollId == pollId);

                currentPoll = await this.dbContext
                    .Polls
                    .FirstOrDefaultAsync(x => x.Id == pollId);
                if (option != null && (currentPoll.PollVotedUsers.Any(x => x.UserId == userId) == false || currentPoll.IsMultichoice == true))
                {
                    option.VotesCount++;
                    currentPoll.PollVotedUsers.Add(new PollVotedUsers
                    {
                        User = user,
                        UserId = userId,
                        PollId = pollId,
                        Poll = currentPoll
                    });

                    await this.dbContext.SaveChangesAsync();
                }
            }

            return currentPoll;
        }

        public async Task<Poll> DeletePoll(string id)
        {
            Poll poll = await this.dbContext
                .Polls
                .FirstOrDefaultAsync(x => x.Id == id);

            poll.IsDeleted = true;
            await this.dbContext.SaveChangesAsync();
            return poll;
        }

        public async Task<Poll> CreatePoll(AddPollInputModel model, string id)
        {
            Poll newPoll = new Poll()
            {
                Content = model.Content,
                ClubId = id,
                IsMultichoice = model.IsMultichoice.ToLower() == "true" ? true
                : false,
                ExpiredDate = model.ExpiredDate,
            };

            List<string> options = model.Options.Split("~", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            foreach (var option in options)
            {
                Option newOption = new Option()
                {
                    VotesCount = 0,
                    Content = option,
                    PollId = newPoll.Id,
                };

                newPoll.Options.Add(newOption);
            }

            var result =  await this.dbContext.Polls.AddAsync(newPoll);
            await this.dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ListPollsBindingModel> GetPollsModel(string clubId, string userId)
        {
            Club club = await this.dbContext
                .Clubs
                .FirstOrDefaultAsync(x => x.Id == clubId);

            ListPollsBindingModel result = new ListPollsBindingModel
            {
                ClubPriceType = club.PriceType.ToString(),
                Club = club,
                Polls = this.dbContext
                        .Polls
                        .Where(x => x.ClubId == clubId && this.IsPollValid(x.ExpiredDate) && !x.IsDeleted)
                        .Select(x => new PollItemBindingModel
                        {
                            Id = x.Id,
                            Content = x.Content,
                            VotesCount = x.PollVotedUsers.Count,
                            ExpiredDate = x.ExpiredDate,
                            Options = x.Options.ToList(),
                            IsMultichoice = x.IsMultichoice,
                            PollVotedUsers = x.PollVotedUsers.ToList()
                        })
                        .ToList(),
                ClubId = club.Id,
                UserId = this.dbContext.Users.First(u => u.Id == userId).Id,
                Messages = await this.dbContext
                .Messages
                .Include(x => x.Sender)
                .Where(x => x.ClubId == clubId)
                .ToListAsync(),
        };

            return result;
        }

        private bool IsPollValid(DateTime expiredDate)
        {
            return expiredDate.Subtract(DateTime.UtcNow).Hours > 0;
        }

        public async Task<AdministrationPollsBindingModel> GetAdministrationBindingModel(string clubId, bool expired)
        {
            Club club = await this.dbContext
                .Clubs
                .FirstOrDefaultAsync(x => x.Id == clubId);

            AdministrationPollsBindingModel result = new AdministrationPollsBindingModel
            {
                Club = club,
                ClubPriceType = club.PriceType.ToString(),
                Polls = this.dbContext
                              .Polls
                              .Where(x => this.IsPollValid(x.ExpiredDate) != expired && x.ClubId == clubId && !x.IsDeleted)
                              .Include(x => x.Options)
                              .ToList(),
                Messages = await this.dbContext
                .Messages
                .Include(x => x.Sender)
                .Where(x => x.ClubId == clubId)
                .ToListAsync(),
                Expired = expired,
        };

            return result;
        }
    }
}
