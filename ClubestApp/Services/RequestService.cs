namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public JoinClubRequest CreateJoinRequestClub(string clubId, User user)
        {
            Club club = this.clubService.GetClubById(clubId);
            JoinClubRequest joinClubRequest = new JoinClubRequest()
            {
                ClubId = clubId,
                Club = club,
                User = user,
                UserId = user.Id
            };

            bool anyPreviousRequest = this.dbContext.JoinClubRequests.Any(x => x.ClubId == clubId && x.UserId == user.Id);
            if (anyPreviousRequest)
            {
                return null;
            }

            this.dbContext.JoinClubRequests.Add(joinClubRequest);
            this.dbContext.SaveChanges();

            return joinClubRequest;
        }

        public IList<JoinClubRequest> GetJoinClubRequestsForAClub(string clubId)
        {
            return this.dbContext.JoinClubRequests
                .Include(x => x.User)
                .Where(x => x.ClubId == clubId && x.RequestType == RequestType.Pending)
                .ToList();
        }

        public JoinClubRequest ApproveJoinClubRequest(string requestId, int requestType)
        {
            JoinClubRequest request = this.dbContext.JoinClubRequests
                .FirstOrDefault(x => x.Id == requestId);

            if (requestType == 1)
            {
                request.RequestType = RequestType.Approved;
            }
            else if (requestType == 2)
            {
                request.RequestType = RequestType.Removed;
            }

            this.dbContext.SaveChanges();

            return request;
        }
    }
}
