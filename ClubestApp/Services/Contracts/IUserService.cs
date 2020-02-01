namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels;
    using System.Collections.Generic;

    public interface IUserService
    {
        User FindUserById(string id);

        User EditUser(EditProfileInputModel model);

        User AddInterestsToUser(AddInterestsInputModel inputModel, string userId);

        string InterestsToString(List<string> interests);
    }
}
