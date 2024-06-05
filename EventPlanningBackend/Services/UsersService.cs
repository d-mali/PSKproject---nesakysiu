﻿using EventBackend.Entities;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDomain.Contracts.Requests;
using Microsoft.EntityFrameworkCore;

namespace EventBackend.Services
{
    public class UsersService : IUsersService
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        protected readonly MainDbContext _context;

        public UsersService(IGenericRepository<ApplicationUser> userRepository, MainDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        public async Task<ApplicationUser> CreateUserAsync(EmployeeRequest entity)
        {
            var evt = new ApplicationUser
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };

            return await _userRepository.InsertAsync(evt);
        }

        public async Task<bool> DeleteUserAsync(String id)
        {
            var participantEntity = await _userRepository.GetByIdAsync(id);

            if (participantEntity == null)
                return false;

            var result = await _userRepository.DeleteAsync(id);

            return result;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(String id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<ApplicationUser?> UpdateUserAsync(String id, EmployeeRequest entity)
        {
            var participantEntity = await _userRepository.GetByIdAsync(id);
            if (participantEntity == null)
                return null;

            participantEntity.FirstName = entity.FirstName;
            participantEntity.LastName = entity.LastName;

            return await _userRepository.UpdateAsync(participantEntity);
        }

        public async Task<EventTask?> CreateTasking(String userId, Guid taskId)
        {
            var useris = await _context.Users.FindAsync(userId);
            var task = await _context.Tasks.FindAsync(taskId);

            if (useris == null || task == null)
            {
                return null;
            }
            useris.Tasks ??= new List<EventTask>();

            useris.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<IEnumerable<EventTask>?> GetUserTasks(string id, Guid eventId)
        {
            var eventWithWorkers = await _context.Users
                .Include(s => s.Tasks)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (eventWithWorkers == null)
            {
                return null;
            }
            if (eventWithWorkers.Tasks == null)
            {
                return null;
            }

            var tasksWithSpecificEventId = eventWithWorkers.Tasks
                .Where(t => t.EventId == eventId)
                .ToList();

            return tasksWithSpecificEventId;
        }

        public async Task<ApplicationUser?> DeleteTasking(String userId, Guid taskId)
        {
            var eventas = await _context.Users
                .Include(s => s.Tasks)
                .FirstOrDefaultAsync(s => s.Id == userId);
            if (eventas == null)
            {
                return null;
            }
            if (eventas.Tasks == null)
            {
                return null;
            }

            var participant = eventas.Tasks.FirstOrDefault(p => p.Id == taskId);
            if (participant == null)
            {
                return null;
            }


            eventas.Tasks ??= new List<EventTask>();

            eventas.Tasks.Remove(participant);

            await _context.SaveChangesAsync();

            eventas.Tasks = null;

            return eventas;
        }
    }
}
