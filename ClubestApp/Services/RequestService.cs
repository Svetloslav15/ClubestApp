namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RequestService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ClubService clubService;

        public RequestService(ApplicationDbContext dbContext,
                           ClubService clubService)
        {
            this.dbContext = dbContext;
            this.clubService = clubService;
        }

        public async Task<JoinClubRequest> CreateJoinRequestClub(string clubId, User user)
        {
            Club club = await this.clubService.GetClubById(clubId);
            JoinClubRequest joinClubRequest = new JoinClubRequest()
            {
                ClubId = clubId,
                Club = club,
                User = user,
                UserId = user.Id
            };

            bool anyPreviousRequest = await this.dbContext.JoinClubRequests.AnyAsync(x => x.ClubId == clubId && x.UserId == user.Id);
            if (anyPreviousRequest)
            {
                return null;
            }

            await this.dbContext.JoinClubRequests.AddAsync(joinClubRequest);
            await this.dbContext.SaveChangesAsync();

            return joinClubRequest;
        }

        public async Task<IList<JoinClubRequest>> GetPendingJoinClubRequestsForAClub(string clubId)
        {
            return await this.dbContext.JoinClubRequests
                .Include(x => x.User)
                .Where(x => x.ClubId == clubId && x.RequestType == RequestType.Pending)
                .ToListAsync();
        }

        public async Task<IList<JoinClubRequest>> GetArchivedAndApprovedJoinClubRequestsForAClub(string clubId)
        {
            return await this.dbContext.JoinClubRequests
                .Include(x => x.User)
                .Where(x => x.ClubId == clubId && (x.RequestType == RequestType.Removed || x.RequestType == RequestType.Approved))
                .ToListAsync();
        }

        public async Task<JoinClubRequest> ApproveJoinClubRequest(string requestId, int requestType)
        {
            JoinClubRequest request = await this.dbContext.JoinClubRequests
                .FirstOrDefaultAsync(x => x.Id == requestId);

            if (requestType == 1)
            {
                UserClub userClub = new UserClub()
                {
                    Club = request.Club,
                    User = request.User,
                    ClubId = request.ClubId,
                    UserId = request.UserId
                };
                await this.dbContext.UserClubs.AddAsync(userClub);
                request.RequestType = RequestType.Approved;
            }
            else if (requestType == 2)
            {
                request.RequestType = RequestType.Removed;
            }
           
            await this.dbContext.SaveChangesAsync();

            return request;
        }

        public async Task<JoinClubRequest> DeleteJoinClubRequest(string requestId)
        {
            JoinClubRequest request = await this.dbContext.JoinClubRequests
                .FirstOrDefaultAsync(x => x.Id == requestId);

            this.dbContext.JoinClubRequests.Remove(request);
            await this.dbContext.SaveChangesAsync();

            return request;
        }
    }
}