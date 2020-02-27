namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Events;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<IList<Event>> GetAllEventsForClub(string clubId)
        {
            IList<Event> events = await this.dbContext.Events
                .Include(x => x.EventUsers)
                .Where(x => x.ClubId == clubId)
                .OrderBy(x => x.Date)
                .ToListAsync();

            return events;
        }

        public async Task<IList<Event>> GetUpCommingEventsForClub(string clubId)
        {
            IList<Event> events = await this.dbContext.Events
                .Include(x => x.EventUsers)
                .Where(x => x.ClubId == clubId && x.Date.Subtract(DateTime.UtcNow).Hours > 0)
                .OrderBy(x => x.Date)
                .ToListAsync();

            return events;
        }

        public async Task<IList<Event>> GetUpExpiredEventsForClub(string clubId)
        {
            IList<Event> events = await this.dbContext.Events
                .Include(x => x.EventUsers)
                .Where(x => x.ClubId == clubId && x.Date.Subtract(DateTime.UtcNow).Hours <= 0)
                .OrderBy(x => x.Date)
                .ToListAsync();

            return events;
        }
    }
}