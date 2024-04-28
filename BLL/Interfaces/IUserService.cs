using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserById(int id);
        public Task<User> GetUserByUsername(string username);
        public Task<User> GetUserByCredentials(string username, string password);
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> CreateUser(User newUser, string password);
        public Task<User> UpdateUser(int id, User updatedUser);
        public Task<User> UpdatePassword(int id, string password);
        public Task<User> DeleteUser(int id);
    }
}
