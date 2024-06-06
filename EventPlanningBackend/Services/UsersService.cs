using EventBackend.Entities;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace EventBackend.Services
{
    public class UsersService(IGenericRepository<ApplicationUser> userRepository, MainDbContext context)
            : IUsersService
    {
        public async Task<ApplicationUser> CreateUserAsync(EmployeeRequest entity)
        {
            var evt = new ApplicationUser
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };

            return await userRepository.InsertAsync(evt);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var participantEntity = await userRepository.GetByIdAsync(id);

            if (participantEntity == null)
                return false;

            var result = await userRepository.DeleteAsync(id);

            return result;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await userRepository.GetAllAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public async Task<ApplicationUser?> UpdateUserAsync(string id, EmployeeRequest entity)
        {
            var participantEntity = await userRepository.GetByIdAsync(id);
            if (participantEntity == null)
                return null;

            participantEntity.FirstName = entity.FirstName;
            participantEntity.LastName = entity.LastName;

            return await userRepository.UpdateAsync(participantEntity);
        }

        public async Task<TaskResponse?> AssignToTask(string userId, Guid taskId)
        {
            var user = await context.Users.FindAsync(userId);
            var task = await context.Tasks.FindAsync(taskId);

            if (user == null || task == null)
            {
                return null;
            }

            user.Tasks.Add(task);

            await context.SaveChangesAsync();

            return task.ToResponse();
        }

        public async Task<IEnumerable<TaskResponse>> GetUserTasks(string userId, Guid? eventId = null)
        {
            var user = await context.Users
                .Include(s => s.Tasks)
                .ThenInclude(t => t.Users)
                .FirstOrDefaultAsync(s => s.Id == userId);

            if (user == null)
            {
                return [];
            }

            if (eventId != null)
            {
                user.Tasks = user.Tasks.Where(t => t.EventId == eventId).ToList();
            }

            return user.Tasks.Select(t => t.ToResponse()).ToList();
        }

        public async Task<ApplicationUser?> RemoveUserFromTask(string userId, Guid taskId)
        {
            var userEntity = await context.Users
                .Include(s => s.Tasks)
                .FirstOrDefaultAsync(s => s.Id == userId);

            var task = userEntity?.Tasks.FirstOrDefault(p => p.Id == taskId);

            if (task == null)
            {
                return null;
            }

            userEntity?.Tasks.Remove(task);

            await context.SaveChangesAsync();

            return userEntity;
        }
    }
}
