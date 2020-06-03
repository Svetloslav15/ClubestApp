namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRequestService
    {
        Task<JoinClubRequest> CreateJoinRequestClub(string clubId, User user);

        Task<IList<JoinClubRequest>> GetPendingJoinClubRequestsForAClub(string clubId);

        Task<IList<JoinClubRequest>> GetArchivedAndApprovedJoinClubRequestsForAClub(string clubId);

        Task<JoinClubRequest> ApproveJoinClubRequest(string requestId, int requestType);

        Task<JoinClubRequest> DeleteJoinClubRequest(string requestId);
    }
}