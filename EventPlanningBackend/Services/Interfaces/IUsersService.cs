using EventBackend.Filters;
using EventBackend.Models.Requests;
using EventDomain.Entities;

namespace EventBackend.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<User> CreateUserAsync(UserRequest entity);
        public Task<IEnumerable<User>> GetAllUsersAsync(UserQuery filter);
        public Task<User> GetUserByIdAsync(Guid id);
        public Task<User> UpdateUserAsync(Guid id, UserRequest entity);
        public Task<bool> DeleteUserAsync(Guid id);
    }
}
