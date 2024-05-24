using System.ComponentModel.DataAnnotations;

namespace EventBackend.Models.Requests
{
    public class ParticipantRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateOnly BirthDate { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
