using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Exceptions;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;
using Szoftverfejlesztés_dotnet_hw.DAL;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Services
{
    public class GroupService : IGroupService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GroupService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Group> AddUserToGroupAsync(int groupId, int userId)
        {
            using var transaction = _context.Database.BeginTransaction();
            var efGroup = await _context.Groups
                .SingleOrDefaultAsync(g => g.Id == groupId)
                ?? throw new EntityByIdNotFoundException("Group with id not found", groupId);
            var efUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId)
                ?? throw new EntityByIdNotFoundException("User with id not found", userId);

            efGroup.Users.Add(efUser);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return await GetGroupByIdAsync(groupId);

        }

        public async Task RemoveUserFromGroup(int groupId, int userId)
        {
            using var transaction = _context.Database.BeginTransaction();
            var efGroup = await _context.Groups
                .Include(g => g.Users)
                .SingleOrDefaultAsync(g => g.Id == groupId)
                ?? throw new EntityByIdNotFoundException("Group with id not found", groupId);
            var efUser = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == userId)
                ?? throw new EntityByIdNotFoundException("User with id not found", userId);

            efGroup.Users.Remove(efUser);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }

        public async Task<Group> CreateGroupAsync(Group group)
        {
            using var transaction = _context.Database.BeginTransaction();
            var creator = await _context.Users.SingleOrDefaultAsync(u => u.Username == group.Creatorname)
                ?? throw new EntityByNameNotFoundException("Creator with username not found", group.Creatorname);

            var createdGroup = new DAL.Entities.Group
            {
                Groupname = group.Groupname,
                Creatorname = group.Creatorname,
            };

            await _context.Groups.AddAsync(createdGroup);
            createdGroup.Users.Add(creator);
            await _context.SaveChangesAsync();
            transaction.Commit();
            return await GetGroupByIdAsync(createdGroup.Id);
        }

        public async Task DeleteGroupAsync(int id)
        {
            var efGroup = await _context.Groups.SingleOrDefaultAsync(g => g.Id == id)
                ?? throw new EntityByIdNotFoundException("Group with id not found", id);
            _context.Groups.Remove(efGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            var groups = await _context.Groups
                .ProjectTo<Group>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return groups;
        }

        public async Task<List<User>> GetAllUsersInGroup(int id)
        {
            var usersInGroup = await _context.Groups
                .Include(g => g.Users)
                .Where(g => g.Id == id)
                .SelectMany(g => g.Users)
                .ProjectTo<User>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return usersInGroup;
        }

        

        public async Task<Group> GetGroupByGroupnameAsync(string groupname)
        {
            var group = await _context.Groups
                .ProjectTo<Group>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(g => g.Groupname == groupname)
                ?? throw new EntityByNameNotFoundException("Group with groupname not found", groupname);

            return group;
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            var group = await _context.Groups
                .ProjectTo<Group>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(g => g.Id == id)
                ?? throw new EntityByIdNotFoundException("Group with id not found", id);

            return group;
        }

        public async Task<Group> UpdateGroupnameAsync(int id, Group updatedGroup)
        {
            using var transaction = _context.Database.BeginTransaction();
            var efGroup = await _context.Groups.SingleOrDefaultAsync(g => g.Id == id)
                ?? throw new EntityByIdNotFoundException("Group with id not found", id);
            
            efGroup.Groupname = updatedGroup.Groupname;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await GetGroupByIdAsync(id);
        }

        public Task<List<Event>> GetAllEventsInGroup(int id)
        {
            var events = _context.Groups
                .Include(g => g.Events)
                .Where(g => g.Id == id)
                .SelectMany(g => g.Events)
                .ProjectTo<Event>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return events;
        }
    }
}
