using EventBackend.Services.Interfaces;
using EventDomain.Entities;

namespace EventBackend.Services
{
    public class ParticipantsService : IParticipantsService
    {
        public Task<Participant> CreateParticipantAsync(Participant entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteParticipantAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Participant>> GetAllParticipantsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Participant> GetParticipantByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Participant> UpdateParticipantAsync(Guid id, Participant entity)
        {
            throw new NotImplementedException();
        }
    }
}
