namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels;
    using System.Collections.Generic;

    public interface IClubService
    {
        ClubAdmin AddClubAdmin(Club club, User user);

        Club AddClub(AddClubInputModel model, string userId);

        Dictionary<string, Dictionary<string, string>> GetInterests();

        List<Club> GetPotentialClubs(string userInterestsString);
    }
}