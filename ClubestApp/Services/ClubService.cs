namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Data.Models.Enums;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
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
        private readonly CloudinaryService cloudinaryService;
        private readonly UserManager<User> userManager;
        private readonly string interestsPath = $"{Directory.GetCurrentDirectory()}/Common/Json/Interests.json";
        private const string defaultPictureUrl = @"https://res.cloudinary.com/dzivpr6fj/image/upload/v1580139315/ClubestPics/identyfying_skills_needs_360x240_p4zsjq.jpg";

        public ClubService(ApplicationDbContext dbContext,
                           UserService userService, CloudinaryService cloudinaryService,
                           UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
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
                sb.Append($"{interest},");
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

        public async Task<ClubAdmin> AddClubAdmin(Club club, User user)
        {
            ClubAdmin newClubAdmin = new ClubAdmin()
            {
                Club = club,
                User = user,
            };

            var result = await dbContext.ClubAdmins.AddAsync(newClubAdmin);
            await this.userManager.AddToRoleAsync(user, "ClubAdmin");
            await this.dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ClubAdmin> RemoveClubAdmin(Club club, User user)
        {
            ClubAdmin clubAdmin = await this.dbContext.ClubAdmins
                .FirstOrDefaultAsync(x => x.User == user && x.Club == club);

            if (clubAdmin != null)
            {
                this.dbContext.ClubAdmins.Remove(clubAdmin);
            }

            await this.userManager.RemoveFromRoleAsync(user, "ClubAdmin");
            await this.dbContext.SaveChangesAsync();
            return clubAdmin;
        }

        public async Task<RequestNewClub> AddRequestNewClub(AddClubInputModel model, string userId)
        {
            string currentUrl = await this.cloudinaryService.UploadImage(model.ImageFile);
            User user = await this.userService.FindUserById(userId);

            RequestNewClub requestNewClub = new RequestNewClub
            {
                Name = model.Name,
                Fee = (decimal)model.Fee,
                PriceType = (PriceType)Enum.Parse(typeof(PriceType), model.PriceType, true),
                IsPublic = (bool)model.IsPublic,
                Description = model.Description,
                Town = model.Town,
                PictureUrl = currentUrl != "" ? currentUrl : defaultPictureUrl,
                Interests = this.userService.InterestsToString(model.Interests),
                Author = user,
                AuthorId = user.Id
            };

            var result = await this.dbContext.RequestNewClubs.AddAsync(requestNewClub);
            await this.dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<List<RequestNewClub>> GetRequestsNewClubAsync()
        {
            return await this.dbContext.RequestNewClubs
                .ToListAsync();
        }

        public async Task<IList<Message>> GetAllMessagesForClub(string id)
        {
            IList<Message> result = await this.dbContext
                .Messages
                .Include(x => x.Sender)
                .Where(x => x.ClubId == id)
                .ToListAsync();

            return result;
        }

        public async Task<Club> AddClub(AddClubInputModel model, string userId)
        {
            string currentUrl = await this.cloudinaryService.UploadImage(model.ImageFile);

            Club newClub = new Club
            {
                Name = model.Name,
                Fee = (decimal)model.Fee,
                PriceType = (PriceType)Enum.Parse(typeof(PriceType), model.PriceType, true),
                IsPublic = (bool)model.IsPublic,
                Description = model.Description,
                Town = model.Town,
                PictureUrl = currentUrl != "" ? currentUrl : defaultPictureUrl,
                Interests = this.userService.InterestsToString(model.Interests)
            };

            var result = await this.dbContext.Clubs.AddAsync(newClub);
            await this.dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Club> EditClub(EditClubInputModel model, string id)
        {
            string currentUrl = await this.cloudinaryService.UploadImage(model.ImageFile);

            Club club = await this.dbContext
                .Clubs
                .FirstOrDefaultAsync(x => x.Id == id);

            club.Name = model.Name;
            club.Fee = (decimal)model.Fee;
            club.PriceType = (PriceType)Enum.Parse(typeof(PriceType), model.PriceType, true);
            club.IsPublic = (bool)model.IsPublic;
            club.Description = model.Description;
            club.Town = model.Town;
            club.PictureUrl = currentUrl != ""
                ? currentUrl
                : model.PictureUrl;
            club.Interests = this.userService.InterestsToString(model.Interests);

            await this.dbContext.SaveChangesAsync();

            return club;
        }

        public async Task<EditClubBindingModel> GetEditClubModel(string id)
        {
            string interests = await this.dbContext
                .Clubs
                .Where(x => x.Id == id)
                .Select(x => x.Interests)
                .FirstOrDefaultAsync();
            List<string> interestsToList = interests.Split(",").ToList();

            var model = await this.dbContext
                .Clubs
                .Where(x => x.Id == id)
                .Select(x => new EditClubBindingModel
                {
                    Name = x.Name,
                    PriceType = x.PriceType.ToString(),
                    Fee = x.Fee,
                    Id = x.Id,
                    Description = x.Description,
                    PictureUrl = x.PictureUrl,
                    Interests = interestsToList,
                    IsPublic = x.IsPublic,
                    Town = x.Town,
                    InterestsToList = this.GetInterests()
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task<ListClubMembersBindingModel> GetMemberInClub(string clubId)
        {
            IList<ClubAdmin> clubAdmins = await this.GetClubAdmins();
            return new ListClubMembersBindingModel()
            {
                Club = await this.GetClubById(clubId),
                ClubPriceType = await this.GetClubPriceType(clubId),
                Members = await this.dbContext
                    .UserClubs
                    .Include(x => x.User)
                    .Include(x => x.Club)
                    .Where(x => x.ClubId == clubId)
                    .Select(x => new MemberItemBindingModel
                    {
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        PictureUrl = x.User.PictureUrl,
                        Id = x.UserId,
                        Town = x.User.Town,
                        Email = x.User.Email,
                        Number = x.User.PhoneNumber,
                    })
                    .ToListAsync(),
                ClubAdmins = clubAdmins,
                Messages = await this.dbContext
                .Messages
                .Include(x => x.Sender)
                .Where(x => x.ClubId == clubId)
                .ToListAsync(),
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

        public async Task<Club> DeleteClubById(string id)
        {
            Club club = await this.GetClubById(id);
            if (club != null)
            {
                this.dbContext.Clubs.Remove(club);
            }

            await this.dbContext.SaveChangesAsync();
            return club;
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

        public bool IsFileValid(IFormFile photoFile)
        {
            string[] validTypes = new string[]
            {
                "image/x-png", "image/gif" , "image/jpeg", "image/jpg", "image/png", "image/gif", "image/svg"
            };

            if (validTypes.Contains(photoFile.ContentType) == false)
            {
                return false;
            }

            return true;
        }

        public async Task<IList<ClubAdmin>> GetClubAdmins()
        {
            return await this.dbContext.ClubAdmins
                .ToListAsync();
        }
    }
}