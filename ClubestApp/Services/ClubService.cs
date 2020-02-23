namespace ClubestApp.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Data.Models.Enums;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class ClubService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserService userService;
        private readonly Cloudinary cloudinary;
        private readonly IConfiguration configuration;
        private readonly string interestsPath = $"{Directory.GetCurrentDirectory()}/Common/Json/Interests.json";
        private const string defaultPictureUrl = @"https://res.cloudinary.com/dzivpr6fj/image/upload/v1580139315/ClubestPics/identyfying_skills_needs_360x240_p4zsjq.jpg";

        public ClubService(ApplicationDbContext dbContext,
                           UserService userService,
                           IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.configuration = configuration;
            this.cloudinary = new Cloudinary(
                                        new Account(
                                             this.configuration.GetConnectionString("CloudinaryCloudName"),
                                             this.configuration.GetConnectionString("CloudinaryApiKey"),
                                             this.configuration.GetConnectionString("CloudinaryAppSecret"))
                                        );
        }

        public async Task<PrivateClubBindingModel> GetClub(string id)
        {
            PrivateClubBindingModel model = await dbContext.Clubs
                .Where(c => c.Id == id)
                .Select(c => new PrivateClubBindingModel
                {
                    Name = c.Name,
                    Fee = c.Fee,
                    Description = c.Description,
                    Town = c.Town,
                    Interests = c.Interests,
                    PictureUrl = c.PictureUrl,
                    PriceType = (PriceType)c.PriceType
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<IList<Club>> GetAllClubs()
        {
            return await this.dbContext.Clubs.ToListAsync();
        }
        public GetClubsBindingModel[] GetAllClubsBindingModel(IList<Club> clubs)
        {
            GetClubsBindingModel[] models = clubs
                .Select(c => new GetClubsBindingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    PictureUrl = c.PictureUrl,
                    Town = c.Town,
                    IsPublic = c.IsPublic
                })
                .ToArray();

            return models;
        }

        private string InterestsToString(List<string> interests)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string interest in interests)
            {
                sb.Append($"{interest}, ");
            }

            return sb.ToString().Trim();
        }

        private List<string> ParseInterests(string interestsString)
        {
            string pattern = "[a-zA-Z-]+";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(interestsString);
            List<string> interests = new List<string>();

            foreach (Match match in matches)
            {
                interests.Add(match.Value);
            }

            return interests;
        }

        //TODO add roles
        public ClubAdmin AddClubAdmin(Club club, User user)
        {
            ClubAdmin newClubAdmin = new ClubAdmin()
            {
                Club = club,
                User = user,
            };

            var result = dbContext.ClubAdmins.Add(newClubAdmin);

            return result.Entity;
        }

        public async Task<Club> AddClub(AddClubInputModel model, string userId)
        {
            //Work on image
            string currentUrl = "";
            if (model.ImageFile != null)
            {
                string filePath = Path.GetFileName(model.ImageFile.FileName);
                using (var stream = File.Create(filePath))
                {
                    model.ImageFile.CopyToAsync(stream)
                        .GetAwaiter()
                        .GetResult();
                }

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    PublicId = $"ClubestPics/{model.ImageFile.FileName}"
                };

                //Deletes file in pc and uploads it in cloud
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                File.Delete(filePath);
                currentUrl = uploadResult.Uri.AbsoluteUri;
            }

            Club newClub = new Club
            {
                Name = model.Name,
                Fee = (decimal)model.Fee,
                PriceType = (PriceType)Enum.Parse(typeof(PriceType), model.PriceType, true),
                IsPublic = (bool)model.IsPublic,
                Description = model.Description,
                Town = model.Town,
                PictureUrl = currentUrl != ""
                ? currentUrl
                : defaultPictureUrl,
                Interests = this.userService.InterestsToString(model.Interests)
            };

            User user = await this.userService.FindUserById(userId);

            this.AddClubAdmin(newClub, user);

            var result = await this.dbContext.Clubs.AddAsync(newClub);
            await this.dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async void AddVote(List<string> votes, string pollId, string userId)
        {
            User user = this.dbContext.Users.First(u => u.Id == userId);
            foreach (var vote in votes)
            {
                Option option = this.dbContext
                    .Options
                    .FirstOrDefault(x => x.Content == vote && x.PollId == pollId);
                Poll currentPoll = this.dbContext
                    .Polls
                    .First(x => x.Id == pollId);
                if (option != null && currentPoll.PollVotedUsers.Any(x => x.UserId == userId) == false)
                {
                    option.VotesCount++;
                    currentPoll.PollVotedUsers.Add(new PollVotedUsers
                    {
                        User = user,
                        UserId = userId,
                        PollId = pollId,
                        Poll = currentPoll
                    });

                    this.dbContext.SaveChanges();
                }
            }

        }

        public async void CreatePoll(AddPollInputModel model, string id)
        {
            Poll newPoll = new Poll()
            {
                Content = model.Content,
                ClubId = id,
                IsMultichoice = model.IsMultichoice.ToLower() == "true" ? true
                : false,
                ExpiredDate = model.ExpiredDate,
            };

            var poll = this.dbContext.Polls.Add(newPoll);

            List<string> options = model.Options.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            List<Option> optionsObjects = new List<Option>();
            foreach (var option in options)
            {
                Option newOption = new Option()
                {
                    VotesCount = 0,
                    Content = option,
                    PollId = poll.Entity.Id,
                };

                this.dbContext.Options.Add(newOption);
            }

            this.dbContext.SaveChanges();
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
                        .Where(x => x.ClubId == clubId && this.IsPollValid(x.ExpiredDate))
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
                UserId = this.dbContext.Users.First(u => u.Id == userId).Id
            };

            return result;
        }

        private bool IsPollValid(DateTime expiredDate)
        {
            return expiredDate.Subtract(DateTime.UtcNow).Hours > 0;
        }

        public async Task<ListClubMemebersBindingModel> GetMemberInClub(string clubId)
        {
            return new ListClubMemebersBindingModel()
            {
                Club = await this.GetClubById(clubId),
                ClubPriceType = await this.GetClubPriceType(clubId),
                Members = await this.dbContext
                    .UserClubs
                    .Where(x => x.ClubId == clubId)
                    .Select(x => new MemberItemBindingModel
                    {
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        PictureUrl = x.User.PictureUrl,
                        Id = x.UserId,
                        Town = x.User.Town,
                        Email = x.User.Email,
                        Number = x.User.PhoneNumber
                    })
                    .ToListAsync()
            };
        }

        private async Task<string> GetClubPriceType(string clubId)
        {
            return await this.dbContext.Clubs
                .Where(x => x.Id == clubId)
                .Select(x => x.PriceType.ToString())
                .FirstOrDefaultAsync();
        }

        public Dictionary<string, Dictionary<string, string>> GetInterests()
        {
            string interestsToText = File.ReadAllText(interestsPath, Encoding.GetEncoding("utf-8"));

            var interestsJson = JsonConvert.DeserializeObject<Dictionary<string,
                                Dictionary<string, string>>>(interestsToText);
            return interestsJson;
        }

        public List<Club> GetPotentialClubs(string userInterestsString, string userTown)
        {
            List<string> userInterests = this.ParseInterests(userInterestsString);
            List<Club> allClubs = this.dbContext.Clubs.ToList();
            List<Club> potentialClubs = new List<Club>();

            foreach (Club club in allClubs)
            {
                Regex regex = new Regex("[a-zA-Z-]+");
                MatchCollection matches = regex.Matches(club.Interests);
                foreach (Match match in matches)
                {
                    string interest = match.Value;
                    if (userInterests.Any(userInterest => userInterest == interest))
                    {
                        potentialClubs.Add(club);
                        break;
                    }
                }
            }
            return potentialClubs;
        }

        public async Task<Club> GetClubById(string clubId)
        {
            return await this.dbContext.Clubs.FirstOrDefaultAsync(club => club.Id == clubId);
        }

        public async Task<IList<Club>> FilterClubsBySearchInput(string userInput)
        {
            userInput = userInput
                .Trim()
                .ToLower();

            IList<Club> clubs = await this.GetAllClubs();
            IList<Club> result = new List<Club>();
            if (userInput == null || userInput == "")
            {
                return clubs;
            }

            foreach (Club club in clubs)
            {
                if (club.Name.ToLower().Contains(userInput) || club.Description.ToLower().Contains(userInput) ||
                    club.Town.ToLower().Contains(userInput) || club.Interests.ToLower().Contains(userInput))
                {
                    result.Add(club);
                }
            }

            return result;
        }
    }
}