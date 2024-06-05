using EventBackend.Entities;
using EventDomain.Contracts.Requests;

namespace EventBackend.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<ApplicationUser> CreateUserAsync(EmployeeRequest entity);
        public Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        public Task<ApplicationUser?> GetUserByIdAsync(Guid id);
        public Task<ApplicationUser?> UpdateUserAsync(Guid id, EmployeeRequest entity);
        public Task<bool> DeleteUserAsync(Guid id);
    }
}
