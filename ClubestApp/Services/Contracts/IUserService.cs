using ClubestApp.Data.Models;
using ClubestApp.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubestApp.Services.Contracts
{
    public interface IUserService
    {
        User FindUserById(string id);

        User EditUser(EditProfileInputModel model);

        User AddInterestsToUser(AddInterestsInputModel inputModel, string userId);

        string InterestsToString(List<string> interests)
    }
}
