using EventBackend.Entities;
using EventBackend.Filters;
using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;

namespace EventBackend.Services
{
    public class UsersService : IUsersService
    {
        public Task<User> CreateUserAsync(UserRequest entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync(UserQuery filter)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(Guid id, UserRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
