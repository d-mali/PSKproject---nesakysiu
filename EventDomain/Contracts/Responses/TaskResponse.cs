using System.ComponentModel.DataAnnotations;

namespace EventDomain.Contracts.Responses
{
    public class TaskResponse
    {
        public Guid Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public DateTime? ScheduledTime { get; set; }

        [Required]
        public required string Description { get; set; }
    }
}
