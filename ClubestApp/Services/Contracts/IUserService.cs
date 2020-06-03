namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels.User;
    using ClubestApp.Models.InputModels;

    using Microsoft.AspNetCore.Http;
    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<User> FindUserById(string id);

        Task<User> EditUser(EditProfileInputModel model);

        Task<User> AddInterestsToUser(AddInterestsInputModel inputModel, string userId);

        Task<Dictionary<string, Dictionary<string, string>>> GetInterests();

        Task<User> ChangeProfilePicture(User user, IFormFile photoFile);

        Task<List<User>> GetAllUsers();

        Task<MyClubsViewModel> GetUsersClubs(string userId);

        string InterestsToString(List<string> interests);

        bool IsFileValid(IFormFile photoFile);

        void RemoveUserAllInterests(User user);
    }
}