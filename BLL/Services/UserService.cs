using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Exceptions;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;
using Szoftverfejlesztés_dotnet_hw.DAL;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Services
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(User newUser, string password)
        {
            var efUser = new DAL.Entities.User
            {
                Username = newUser.Username,
                Password = password,                
            };
            await _context.Users.AddAsync(efUser);
            await _context.SaveChangesAsync();
            return await GetUserByIdAsync(efUser.Id);
        }

        public async Task DeleteUserAsync(int id)
        {
            var userToBeDeleted = await _context.Users.SingleOrDefaultAsync(u => u.Id == id)
                ?? throw new EntityByIdNotFoundException("User with id not found", id);

            _context.Users.Remove(userToBeDeleted);
            await _context.SaveChangesAsync();

        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .ProjectTo<User>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .ProjectTo<User>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(u => u.Id == id)
                ?? throw new EntityByIdNotFoundException("User with id not found", id);

            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .ProjectTo<User>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(u => u.Username == username)
                ?? throw new EntityByNameNotFoundException("User with username not found", username);

            return user;
        }

        public async Task<string> GetUserPasswordAsync(string username)
        {
            var EfUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == username)
                ?? throw new EntityByNameNotFoundException("User with username not found", username);

            return EfUser.Password;
        }

        public async Task<User> UpdatePasswordAsync(int id, string password)
        {
            using var transaction = _context.Database.BeginTransaction();

            var efUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == id)
                ?? throw new EntityByIdNotFoundException("User with id not found", id);

            efUser.Password = password;
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return await GetUserByIdAsync(id);
        }

        public async Task<User> UpdateUserAsync(int id, User updatedUser)
        {
            using var transaction = _context.Database.BeginTransaction();

            var efUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == id)
                ?? throw new EntityByIdNotFoundException("User with id not found", id);

            efUser.Username = updatedUser.Username;
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return await GetUserByIdAsync(id);
        }
    }
}
