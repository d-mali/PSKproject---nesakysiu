using EventDomain.Contracts.Responses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBackend.Entities
{
    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public DateOnly BirthDate { get; set; }

        [Required]
        public required string Email { get; set; }

        [Timestamp]
        public byte[]? Version { get; set; }

        public List<Event>? Events { get; set; }

        public ParticipantResponse ToResponse()
        {
            return new ParticipantResponse
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate,
                Email = Email
            };
        }
    }
}
