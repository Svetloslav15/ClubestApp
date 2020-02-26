namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Events;
    using System.Threading.Tasks;

    public class EventService
    {
        private readonly ApplicationDbContext dbContext;

        public EventService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Event> AddEvent(AddEventInputModel inputModel)
        {
            Event eventEntity = new Event() 
            { 
                Name = inputModel.Name,
                Date = inputModel.Date,
                Description = inputModel.Description,
                IsPublic = inputModel.IsPublic,
                Interests = inputModel.Interests,
                ClubId = inputModel.ClubId
            };

            await this.dbContext.Events.AddAsync(eventEntity);
            await this.dbContext.SaveChangesAsync();
            return eventEntity;
        }
    }
}