using System.ComponentModel.DataAnnotations;

namespace EventBackend.Models.Requests
{
    public class EventTaskRequest
    {
        [Required]
        public required string Title { get; set; }

        public DateTime? ScheduledTime { get; set; }

        public string? Description { get; set; }
    }
}
