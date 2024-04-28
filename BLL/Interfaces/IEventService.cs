using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Interfaces
{
    public interface IEventService
    {
        public Task<Event> GetEventById(int id);
        public Task<IEnumerable<Event>> GetAllEvents();
        public Task<Event> CreateEvent(Event newEvent);
        public Task<Event> UpdateEvent(int id, Event updatedEvent);
        public Task<Event> DeleteEvent(int id);
    }
}
