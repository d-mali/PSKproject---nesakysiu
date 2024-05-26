using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EventBackend.Entities
{
    public class EventTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("EventId")]
        public Guid EventId { get; set; }

        [Required]
        public required string Title { get; set; }

        public DateTime? ScheduledTime { get; set; }

        [Required]
        public required string Description { get; set; }

        [Timestamp]
        public byte[]? Version { get; set; }

        [Required]
        public virtual Event? Event { get; set; }

        public EventTask()
        {
        }

        [SetsRequiredMembers]
        public EventTask(TaskRequest taskRequest)
        {
            Title = taskRequest.Title;
            ScheduledTime = taskRequest.ScheduledTime;
            Description = taskRequest.Description;
            EventId = taskRequest.EventId;
        }

        public TaskResponse ToResponse()
        {
            return new TaskResponse
            {
                Id = Id,
                Title = Title,
                ScheduledTime = ScheduledTime,
                Description = Description,
                EventId = EventId
            };
        }
    }
}
