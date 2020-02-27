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
        private readonly UserService userService;
        private readonly EmailService emailService;

        public EventService(ApplicationDbContext dbContext,
            UserService userService, EmailService emailService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.emailService = emailService;
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

        public async Task<Event> GetEventById(string id)
        {
            return await this.dbContext.Events
                .Include(x => x.EventUsers)
                .FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<Event> JoinEvent(string eventId, string userId)
        {
            Event eventEntity = await this.GetEventById(eventId);
            User user = await this.userService.FindUserById(userId);

            this.emailService.SendEmail(user, $"Успешно се записа за събитието {eventEntity.Name}!", $"Clubest - {eventEntity.Name}");

            if (eventEntity != null && user != null)
            {
                EventUser eventUser = new EventUser()
                {
                    EventId = eventId,
                    Event = eventEntity,
                    UserId = userId,
                    User = user
                };

                await this.dbContext.EventUsers.AddAsync(eventUser);
                await this.dbContext.SaveChangesAsync();
            }

            return eventEntity;
        }

        public async Task<EventUser> ExitEvent(string eventId, string userId)
        {
            EventUser eventUser = await this.dbContext.EventUsers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.EventId == eventId);

            if (eventUser != null)
            {
                this.dbContext.EventUsers.Remove(eventUser);
                await this.dbContext.SaveChangesAsync();
            }

            return eventUser;
        }

        public async Task<Event> DeleteEvent(string eventId)
        {
            Event eventEntity = await this.GetEventById(eventId);

            this.dbContext.Remove(eventEntity);
            await this.dbContext.SaveChangesAsync();

            return eventEntity;
        }
    }
}