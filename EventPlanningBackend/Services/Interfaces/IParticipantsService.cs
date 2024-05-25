using EventDomain.Entities;

namespace EventBackend.Services.Interfaces
{
    public interface IParticipantsService
    {
        public Task<Participant> CreateParticipantAsync(Participant entity);
        public Task<IEnumerable<Participant>> GetAllParticipantsAsync();
        public Task<Participant> GetParticipantByIdAsync(Guid id);
        public Task<Participant> UpdateParticipantAsync(Guid id, Participant entity);
        public Task<bool> DeleteParticipantAsync(Guid id);
    }
}
