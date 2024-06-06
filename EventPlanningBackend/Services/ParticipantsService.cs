using EventBackend.Entities;
using EventBackend.Filters;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EventBackend.Services
{
    public class ParticipantsService(IGenericRepository<Participant> participantRepository) : IParticipantsService
    {
        public async Task<Participant> CreateParticipantAsync(Participant entity)
        {
            var evt = new Participant
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                Email = entity.Email,
            };

            return await participantRepository.InsertAsync(evt);
        }

        public async Task<bool> DeleteParticipantAsync(Guid id)
        {
            var participantEntity = await participantRepository.GetByIdAsync(id);

            if (participantEntity == null)
                return false;

            var result = await participantRepository.DeleteAsync(id);

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

            orderByEvent = filter.Sort switch
            {
                Sorting.Desc => x => x.OrderByDescending(p => EF.Property<Participant>(p, filter.OrderBy.ToString())),
                Sorting.Asc => x => x.OrderBy(p => EF.Property<Participant>(p, filter.OrderBy.ToString())),
                _ => orderByEvent
            };

            return await participantRepository.GetAllAsync(eventFilter, orderByEvent, filter.Skip, filter.Take);
        }

        public async Task<Participant?> GetParticipantByIdAsync(Guid id)
        {
            return await participantRepository.GetByIdAsync(id);
        }

        public async Task<Participant?> UpdateParticipantAsync(Guid id, Participant entity)
        {
            var participantEntity = await participantRepository.GetByIdAsync(id);
            if (participantEntity == null)
                return null;

            participantEntity.FirstName = entity.FirstName;
            participantEntity.LastName = entity.LastName;
            participantEntity.BirthDate = entity.BirthDate;
            participantEntity.Email = entity.Email;

            await participantRepository.UpdateAsync(participantEntity);

            return participantEntity;
        }
    }
}
