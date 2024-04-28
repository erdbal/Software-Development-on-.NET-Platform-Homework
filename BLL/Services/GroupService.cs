using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;
using Szoftverfejlesztés_dotnet_hw.DAL;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Services
{
    public class GroupService : IGroupService
    {

        private readonly AppDbContext _context;

        public GroupService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Group> CreateGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public Task<Group> DeleteGroup(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetAllGroups()
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetGroupByGroupname(string groupname)
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetGroupById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Group> UpdateGroupname(int id, Group updatedGroup)
        {
            throw new NotImplementedException();
        }
    }
}
