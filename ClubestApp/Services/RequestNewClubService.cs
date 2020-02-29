namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class RequestNewClubService
    {
        private readonly ApplicationDbContext dbContext;

        public RequestNewClubService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RequestNewClub> GetRequestNewClubById(string id)
        {
            return await this.dbContext.RequestNewClubs
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<RequestNewClub> Delete(string id)
        {
            RequestNewClub request = await this.GetRequestNewClubById(id);
            this.dbContext.RequestNewClubs.Remove(request);
            await this.dbContext.SaveChangesAsync();

            return request;
        }

        public async Task<RequestNewClub> ApproveAndMakeClub(string id)
        {
            RequestNewClub request = await this.GetRequestNewClubById(id);

            Club club = new Club
            {
                Name = request.Name,
                Fee = (decimal)request.Fee,
                PriceType = request.PriceType,
                IsPublic = request.IsPublic,
                Description = request.Description,
                Town = request.Town,
                PictureUrl = request.PictureUrl,
                Interests = request.Interests
            };

            await this.dbContext.Clubs.AddAsync(club);
            this.dbContext.RequestNewClubs.Remove(request);
            await this.dbContext.SaveChangesAsync();

            return request;
        }
    }
}
