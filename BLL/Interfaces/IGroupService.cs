using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Interfaces
{
    public interface IGroupService
    {
        public Task<Group> GetGroupById(int id);
        public Task<Group> GetGroupByGroupname(string groupname);
        public Task<IEnumerable<Group>> GetAllGroups();
        public Task<Group> CreateGroup(Group group);
        public Task<Group> UpdateGroupname(int id, Group updatedGroup);
        public Task<Group> DeleteGroup(int id);
    }
}
