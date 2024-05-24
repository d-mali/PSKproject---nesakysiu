using System.ComponentModel.DataAnnotations;

namespace EventBackend.Models.Requests
{
    public class TaskRequest
    {
        [Required]
        public required string Title { get; set; }

        public DateTime? ScheduledTime { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
