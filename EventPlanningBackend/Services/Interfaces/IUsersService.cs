using EventBackend.Entities;
using EventDomain.Contracts.Requests;

namespace EventBackend.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<ApplicationUser> CreateUserAsync(EmployeeRequest entity);
        public Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        public Task<ApplicationUser?> GetUserByIdAsync(string id);
        public Task<ApplicationUser?> UpdateUserAsync(string id, EmployeeRequest entity);
        public Task<bool> DeleteUserAsync(string id);
        public Task<EventTask?> CreateTasking(String userId, Guid taskId);
        public Task<IEnumerable<EventTask>?> GetUserTasks(string id, Guid eventId);
        public Task<ApplicationUser?> DeleteTasking(String userId, Guid taskId);
    }
}
