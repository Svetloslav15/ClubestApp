namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService
    {
        private ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User FindUserByUsername(string username)
        {
            return this.dbContext.Users.FirstOrDefault(user => user.UserName == username); 
        }

        public User EditUser(EditProfileInputModel model)
        {
            User user = this.dbContext.Users.FirstOrDefault(userDb => userDb.UserName == model.Username);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            this.dbContext.SaveChanges();
            return user;
        }
    }
}