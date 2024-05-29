using EventBackend.Entities;
using EventBackend.Filters;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EventBackend.Services
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly IGenericRepository<Participant> _participantRepository;

        public ParticipantsService(IGenericRepository<Participant> participantRepository)
        {
            _participantRepository = participantRepository;
        }
        public async Task<Participant> CreateParticipantAsync(Participant entity)
        {
            var evt = new Participant
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                Email = entity.Email,
            };

            return await _participantRepository.InsertAsync(evt);
        }

        public async Task<bool> DeleteParticipantAsync(Guid id)
        {
            var participantEntity = await _participantRepository.GetByIdAsync(id);

            if (participantEntity == null)
                return false;

            var result = await _participantRepository.DeleteAsync(id);

            return result;
        }

        public async Task<IEnumerable<Participant>> GetAllParticipantsAsync(ParticipantQuery filter)
        {
            var eventFilter = PredicateBuilder.True<Participant>();
            Func<IQueryable<Participant>, IOrderedQueryable<Participant>>? orderByEvent = null;

            if (filter.FirstName != null)
            {
                eventFilter = eventFilter.And(x => x.FirstName == filter.FirstName);
            }

            if (filter.LastName != null)
            {
                eventFilter = eventFilter.And(x => x.LastName == filter.LastName);
            }

            if (filter.BirthDate != null)
            {
                eventFilter = eventFilter.And(x => x.BirthDate == filter.BirthDate);
            }

            switch (filter.Sort)
            {
                case Sorting.Desc:
                    orderByEvent = x => x.OrderByDescending(p => EF.Property<Participant>(p, filter.OrderBy.ToString()));
                    break;
                default:
                    orderByEvent = x => x.OrderBy(p => EF.Property<Participant>(p, filter.OrderBy.ToString()));
                    break;
            }

            return await _participantRepository.GetAllAsync(eventFilter, orderByEvent, filter.Skip, filter.Take);
        }

        public async Task<Participant?> GetParticipantByIdAsync(Guid id)
        {
            return await _participantRepository.GetByIdAsync(id);
        }

        public Task<Participant> UpdateParticipantAsync(Guid id, Participant entity)
        {
            throw new NotImplementedException();
        }
    }
}
