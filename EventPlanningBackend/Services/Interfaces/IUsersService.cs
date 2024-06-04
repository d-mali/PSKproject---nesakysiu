using EventBackend.Entities;
using EventBackend.Filters;
using EventBackend.Models.Requests;

namespace EventBackend.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<ApplicationUser> CreateUserAsync(UserRequest entity);
        public Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(UserQuery filter);
        public Task<ApplicationUser> GetUserByIdAsync(Guid id);
        public Task<ApplicationUser> UpdateUserAsync(Guid id, UserRequest entity);
        public Task<bool> DeleteUserAsync(Guid id);
    }
}
