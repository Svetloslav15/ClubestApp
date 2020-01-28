﻿namespace ClubestApp.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Data.Models.Enums;
    using ClubestApp.Models.InputModels;
<<<<<<< HEAD
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Configuration;
=======
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
>>>>>>> 999826864d61fa4f34c9ac009b279bc5dfff391f
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class ClubService
    {
        private ApplicationDbContext dbContext;
        private readonly UserService userService;
        private const string defaultPictureUrl = @"https://res.cloudinary.com/dzivpr6fj/image/upload/v1580139315/ClubestPics/identyfying_skills_needs_360x240_p4zsjq.jpg";
        private Cloudinary cloudinary;
        private IConfiguration configuration;
        private readonly string interestsPath = $"{Directory.GetCurrentDirectory()}/Common/Json/Interests.json";

        public ClubService(ApplicationDbContext dbContext,
                           UserService userService,
                           IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.configuration = configuration;
            this.cloudinary = new Cloudinary(
                                        new Account(
                                             configuration.GetConnectionString("CloudinaryCloudName"),
                                             configuration.GetConnectionString("CloudinaryApiKey"),
                                             configuration.GetConnectionString("CloudinaryAppSecret"))
                                        );
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
                    model.ImageFile.CopyToAsync(stream).GetAwaiter().GetResult();
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

        private string InterestsToString(List<string> interests)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string interest in interests)
            {
                sb.Append($"{interest}, ");
            }

            return sb.ToString().Trim();
        }
        public Dictionary<string, Dictionary<string, string>> GetInterests()
        {
            string interestsToText = File.ReadAllText(interestsPath, Encoding.GetEncoding("windows-1251"));

            var interestsJson = JsonConvert.DeserializeObject<Dictionary<string,
                                Dictionary<string, string>>>(interestsToText);
            return interestsJson;
        }
    }
}