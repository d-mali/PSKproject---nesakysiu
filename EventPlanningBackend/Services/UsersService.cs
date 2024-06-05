using EventBackend.Entities;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDomain.Contracts.Requests;

namespace EventBackend.Services
{
    public class UsersService : IUsersService
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public UsersService(IGenericRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
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
    }
}
