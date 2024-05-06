using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByUsernameAsync(string username);
        public Task<List<User>> GetAllUsersAsync();
        public Task<string> GetUserPasswordAsync(string username);
        public Task<User> CreateUserAsync(User newUser, string password);
        public Task<User> UpdateUserAsync(int id, User updatedUser);
        public Task<User> UpdatePasswordAsync(int id, string password);
        public Task<List<Group>> GetGroupsOfUserAsync(int id);
        public Task DeleteUserAsync(int id);
    }
}
