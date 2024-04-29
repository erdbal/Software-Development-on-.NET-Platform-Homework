using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Interfaces
{
    public interface IGroupService
    {
        public Task<Group> GetGroupByIdAsync(int id);
        public Task<Group> GetGroupByGroupnameAsync(string groupname);
        public Task<List<Group>> GetAllGroupsAsync();
        public Task<Group> CreateGroupAsync(Group group);
        public Task AddUserToGroupAsync(int groupid, int userId);
        public Task RemoveUserFromGroup(int groupid, int userId);
        public Task<List<User>> GetAllUsersInGroup(int id);
        public Task<Group> UpdateGroupnameAsync(int id, Group updatedGroup);
        public Task DeleteGroupAsync(int id);
    }
}
