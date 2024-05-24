using System.ComponentModel.DataAnnotations;

namespace EventBackend.Models.Requests
{
    public class EventTaskRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public DateTime? ScheduledTime { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
