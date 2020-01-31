namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class UserService : IUserService
    {
        private readonly SignInManager<User> signInManager;
        private ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext,
                           SignInManager<User> signInManager)
        {
            this.dbContext = dbContext;
            this.signInManager = signInManager;
        }

        public User FindUserById(string id)
        {
            User userdb = this.dbContext.Users.FirstOrDefault(user => user.Id == id);
            return userdb; 
        }

        public User EditUser(EditProfileInputModel model)
        {
            dbContext.Database.EnsureCreated();

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
    }
}