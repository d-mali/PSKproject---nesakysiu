using System.ComponentModel.DataAnnotations;

namespace EventDomain.Contracts.Requests
{
    public class ParticipantRequest
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public DateOnly BirthDate { get; set; }

        [Required]
        public required string Email { get; set; }
    }
}
