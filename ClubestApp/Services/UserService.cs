namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService
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
            var userdb = this.dbContext.Users.FirstOrDefault(user => user.Id == id);
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
    }
}