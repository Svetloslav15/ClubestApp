namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Events;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventService
    {
        Task<Event> AddEvent(AddEventInputModel inputModel);

        Task<Event> GetEventById(string id);

        Task<IList<Event>> GetAllEventsForClub(string clubId);

        Task<IList<Event>> GetUpCommingEventsForClub(string clubId);

        Task<IList<Event>> GetUpExpiredEventsForClub(string clubId);

        Task<bool> JoinEvent(string eventId, string userId, string role);

        Task<EventUser> ExitEvent(string eventId, string userId);

        Task<Event> DeleteEvent(string eventId);

        Task<IList<EventUser>> GetEventUsersForUser(string userId);
    }
}