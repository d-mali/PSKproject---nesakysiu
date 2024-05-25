using System.ComponentModel.DataAnnotations;

namespace EventBackend.Models.Requests
{
    public class TaskResponse
    {
        [Required]
        public required string Title { get; set; }

        public DateTime? ScheduledTime { get; set; }

        [Required]
        public required string Description { get; set; }
    }
}
