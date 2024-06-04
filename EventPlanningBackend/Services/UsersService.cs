using EventBackend.Entities;
using EventBackend.Filters;
using EventBackend.Models.Requests;
using EventBackend.Services.Interfaces;

namespace EventBackend.Services
{
    public class UsersService : IUsersService
    {
        public Task<ApplicationUser> CreateUserAsync(UserRequest entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(UserQuery filter)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> UpdateUserAsync(Guid id, UserRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
