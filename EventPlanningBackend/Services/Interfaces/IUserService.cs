using EventDomain.Entities;

namespace EventBackend.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> CreateUserAsync(User entity);
        public Task<IEnumerable<User>> GetAllUserAsync();
        public Task<User> GetUserByIdAsync(Guid id);
        public Task<User> UpdateUserAsync(Guid id, User entity);
        public Task<bool> DeleteUserAsync(Guid id);
    }
}
