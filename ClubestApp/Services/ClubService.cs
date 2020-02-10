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
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

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

        public PrivateClubBindingModel GetClub(string id)
        {
            PrivateClubBindingModel model = dbContext.Clubs
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
                .FirstOrDefault();

            return model;
        }

        public IList<Club> GetAllClubs()
        {
            return this.dbContext.Clubs.ToList();
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

        public Club AddClub(AddClubInputModel model, string userId)
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
                var uploadResult = cloudinary.Upload(uploadParams);
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

            User user = this.userService.FindUserById(userId);

            this.AddClubAdmin(newClub, user);

            var result = this.dbContext.Clubs.Add(newClub);
            this.dbContext.SaveChanges();

            return result.Entity;
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

        public Club GetClubById(string clubId)
        {
            return this.dbContext.Clubs.FirstOrDefault(club => club.Id == clubId);
        }

        public IList<Club> FilterClubsBySearchInput(string userInput)
        {
            userInput = userInput
                .Trim()
                .ToLower();

            IList<Club> clubs = this.GetAllClubs();
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