namespace ClubestApp.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class UserService
    {
        private readonly SignInManager<User> signInManager;
        private readonly Cloudinary cloudinary;
        private readonly IConfiguration configuration;
        private const string defaultPictureUrl = @"https://res.cloudinary.com/dzivpr6fj/image/upload/v1580902697/ClubestPics/24029_llq8xg.png";
        private readonly ApplicationDbContext dbContext;
        private readonly string interestsPath = $"{Directory.GetCurrentDirectory()}/Common/Json/Interests.json";

        public UserService(ApplicationDbContext dbContext,
                           SignInManager<User> signInManager,
                           IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.cloudinary = new Cloudinary(
                                        new Account(
                                             this.configuration.GetConnectionString("CloudinaryCloudName"),
                                             this.configuration.GetConnectionString("CloudinaryApiKey"),
                                             this.configuration.GetConnectionString("CloudinaryAppSecret"))
                                        );
        }

        public async Task<User> FindUserById(string id)
        {
            User userdb = await this.dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
            return userdb; 
        }

        public async Task<User> EditUser(EditProfileInputModel model)
        {
            User user = await this.dbContext.Users.FirstOrDefaultAsync(userDb => userDb.UserName == model.Username);
            bool existWithSameUsername = false;
            if (user.UserName != user.Email)
            {
                existWithSameUsername = await this.dbContext.Users.AnyAsync(userDb => userDb.UserName == model.Email);
            }

            if (!existWithSameUsername)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.Email;
                user.NormalizedUserName = model.Email.ToUpper();
                user.Email = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.PhoneNumber = model.PhoneNumber;

                await this.dbContext.SaveChangesAsync();
            }        

            return user;
        }

        public async Task<User> AddInterestsToUser(AddInterestsInputModel inputModel, string userId)
        {
            User user = null;

            if (inputModel.Interests.Count != 0)
            {
                user = await this.FindUserById(userId);
                user.Interests = InterestsToString(inputModel.Interests);

                await this.dbContext.SaveChangesAsync();
            }

            return user;
        }

        public string InterestsToString(List<string> interests)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string interest in interests)
            {
                sb.Append($"{interest}, ");
            }

            return sb.ToString();
        }

        public async Task<Dictionary<string, Dictionary<string, string>>> GetInterests()
        {
            string interestsToText = await File.ReadAllTextAsync(interestsPath, Encoding.GetEncoding("utf-8"));

            var interestsJson = JsonConvert.DeserializeObject<Dictionary<string,
                                Dictionary<string, string>>>(interestsToText);
            return interestsJson;
        }

        public async Task<User> ChangeProfilePicture(User user, IFormFile photoFile)
        {
            //Work on image
            string currentUrl = "";
            if (photoFile != null)
            {
                string filePath = Path.GetFileName(photoFile.FileName);
                using (var stream = File.Create(filePath))
                {
                    photoFile.CopyToAsync(stream)
                        .GetAwaiter()
                        .GetResult();
                }

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    PublicId = $"ClubestPics/{photoFile.FileName}"
                };

                //Deletes file in pc and uploads it in cloud
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                File.Delete(filePath);
                currentUrl = uploadResult.Uri.AbsoluteUri;
            }

            user.PictureUrl = currentUrl != ""
                ? currentUrl
                : defaultPictureUrl;

            await this.dbContext.SaveChangesAsync();

            return user;
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

        public void RemoveUserAllInterests(User user)
        {
            this.dbContext
                .Users
                .FirstOrDefault(x => x.Id == user.Id)
                .Interests = "";

            this.dbContext.SaveChanges();
        }
    }
}