using System.ComponentModel.DataAnnotations;

namespace EventBackend.Models.Requests
{
    /*
     * Basically an EventDTO, storing info about an event from within a request
     * We could use Event entity, but this separates the entity structure from the request structure
     * in case they don't reflect one another
     */
    public class EventRequest
    {
        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
