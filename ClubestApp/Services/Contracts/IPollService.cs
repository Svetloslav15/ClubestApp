namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPollService
    {
        Task<Poll> AddVote(List<string> votes, string pollId, string userId);

        Task<Poll> DeletePoll(string id);

        Task<Poll> CreatePoll(AddPollInputModel model, string id);

        Task<ListPollsBindingModel> GetPollsModel(string clubId, string userId);

        Task<AdministrationPollsBindingModel> GetAdministrationBindingModel(string clubId, bool expired);
    }
}