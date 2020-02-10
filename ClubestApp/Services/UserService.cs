namespace ClubestApp.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class UserService
    {
        private readonly SignInManager<User> signInManager;
        private readonly Cloudinary cloudinary;
        private readonly IConfiguration configuration;
        private const string defaultPictureUrl = @"https://res.cloudinary.com/dzivpr6fj/image/upload/v1580902697/ClubestPics/24029_llq8xg.png";
        private readonly ApplicationDbContext dbContext;

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

        public User FindUserById(string id)
        {
            User userdb = this.dbContext.Users.FirstOrDefault(user => user.Id == id);
            return userdb; 
        }

        public User EditUser(EditProfileInputModel model)
        {
            User user = this.dbContext.Users.FirstOrDefault(userDb => userDb.UserName == model.Username);
            bool existWithSameUsername = false;
            if (user.UserName != user.Email)
            {
                existWithSameUsername = this.dbContext.Users.Any(userDb => userDb.UserName == model.Email);
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

                this.dbContext.SaveChanges();
            }        

            return user;
        }

        public User AddInterestsToUser(AddInterestsInputModel inputModel, string userId)
        {
            User user = null;

            if (inputModel.Interests.Count != 0)
            {
                user = this.FindUserById(userId);
                user.Interests = InterestsToString(inputModel.Interests);

                this.dbContext.SaveChanges();
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

            return sb.ToString().Trim();
        }

        public User ChangeProfilePicture(User user, IFormFile photoFile)
        {
            //Work on image
            string currentUrl = "";
            if (photoFile != null)
            {
                string filePath = Path.GetFileName(photoFile.FileName);
                using (var stream = File.Create(filePath))
                {
                    photoFile.CopyToAsync(stream).GetAwaiter().GetResult();
                }

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    PublicId = $"ClubestPics/{photoFile.FileName}"
                };

                //Deletes file in pc and uploads it in cloud
                var uploadResult = cloudinary.Upload(uploadParams);
                File.Delete(filePath);
                currentUrl = uploadResult.Uri.AbsoluteUri;
            }

            user.PictureUrl = currentUrl != ""
                ? currentUrl
                : defaultPictureUrl;

            this.dbContext.SaveChanges();

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
    }
}