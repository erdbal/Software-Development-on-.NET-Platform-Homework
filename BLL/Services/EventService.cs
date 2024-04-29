using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Exceptions;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;
using Szoftverfejlesztés_dotnet_hw.DAL;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Services
{
    public class EventService : IEventService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EventService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            var createdEvent = new DAL.Entities.Event
            {                
                Eventname = newEvent.Eventname,
                Date = newEvent.Date,
                Description = newEvent.Description,
                Location = newEvent.Location,
            };
            await _context.Events.AddAsync(createdEvent);
            await _context.SaveChangesAsync();
            return await GetEventByIdAsync(createdEvent.Id);

        }



        public async Task DeleteEventAsync(int id)
        {
            var efEvent = _context.Events.SingleOrDefault(e => e.Id == id)
                ?? throw new EntityByIdNotFoundException("Event with id not found",id);
            _context.Events.Remove(efEvent);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            var events = await _context.Events
                .ProjectTo<Event>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return events;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            var DtoEvent = await _context.Events
                .ProjectTo<Event>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(e => e.Id == id)
                ?? throw new EntityByIdNotFoundException("Event with id not found", id);

            return DtoEvent;
        }


        public async Task<List<Event>> GetEventsByGroupIdAsync(int groupId)
        {
            var events = await _context.Events
                .Where(e => e.Group.Id == groupId)
                .ProjectTo<Event>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return events;
        }


        public async Task<List<Event>> GetEventsByGroupnameAsync(string groupname)
        {
            var events = await _context.Events
                .Where(e => e.Group.Groupname == groupname)
                .ProjectTo<Event>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return events;
        }

        public async Task<Event> UpdateEventAsync(int id, Event updatedEvent)
        {
            using var transaction = _context.Database.BeginTransaction();
            var efEvent = await _context.Events.SingleOrDefaultAsync(e => e.Id == id)
                ?? throw new EntityByIdNotFoundException("Event with id not found", id);
            
            efEvent.Eventname = updatedEvent.Eventname;
            efEvent.Date = updatedEvent.Date;
            efEvent.Description = updatedEvent.Description;
            efEvent.Location = updatedEvent.Location;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return await GetEventByIdAsync(id);
        }
    }
}
