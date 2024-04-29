using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Interfaces
{
    public interface IEventService
    {
        public Task<Event> GetEventByIdAsync(int id);
        public Task<List<Event>> GetEventsByGroupIdAsync(int groupId);
        public Task<List<Event>> GetEventsByGroupnameAsync(string groupname);
        public Task<List<Event>> GetAllEventsAsync();
        public Task<Event> CreateEventAsync(Event newEvent);
        public Task<Event> UpdateEventAsync(int id, Event updatedEvent);
        public Task DeleteEventAsync(int id);
    }
}
