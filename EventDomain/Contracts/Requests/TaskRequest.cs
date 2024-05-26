using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TaskStatus = EventDomain.Enums.TaskStatus;

namespace EventDomain.Contracts.Requests
{
    public class TaskRequest
    {
        [Required]
        public required string Title { get; set; }

        public DateTime? ScheduledTime { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public Guid EventId { get; set; }

        [DefaultValue(TaskStatus.ToDo)]
        public TaskStatus? Status { get; set; }
    }
}
