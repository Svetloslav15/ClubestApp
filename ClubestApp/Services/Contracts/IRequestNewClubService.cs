namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;

    using System.Threading.Tasks;

    public interface IRequestNewClubService
    {
        Task<RequestNewClub> GetRequestNewClubById(string id);

        Task<RequestNewClub> Delete(string id);

        Task<RequestNewClub> ApproveAndMakeClub(string id);
    }
}