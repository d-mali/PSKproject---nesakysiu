using EventDomain.Contracts.Requests;
using EventDomain.Contracts.Responses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EventBackend.Entities
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Timestamp]
        public byte[]? Version { get; set; }

        public virtual ICollection<EventTask> Tasks { get; set; } = [];

        public virtual List<Participant> Participants { get; set; } = [];


        public virtual List<ApplicationUser> Users { get; set; } = [];

        public Event()
        {
        }

        [SetsRequiredMembers]
        public Event(EventRequest eventRequest)
        {
            Title = eventRequest.Title;
            Description = eventRequest.Description;
            StartDate = eventRequest.StartDate;
            EndDate = eventRequest.EndDate;
        }

        public EventResponse ToResponse()
        {
            return new EventResponse
            {
                Id = Id,
                Title = Title,
                Description = Description,
                StartDate = StartDate,
                EndDate = EndDate
            };
        }
    }
}
