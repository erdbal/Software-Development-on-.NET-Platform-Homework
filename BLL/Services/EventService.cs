using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;
using Szoftverfejlesztés_dotnet_hw.DAL;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Services
{
    public class EventService : IEventService
    {

        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Event> CreateEvent(Event newEvent)
        {
            throw new NotImplementedException();
        }

        public Task<Event> DeleteEvent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> UpdateEvent(int id, Event updatedEvent)
        {
            throw new NotImplementedException();
        }
    }
}
