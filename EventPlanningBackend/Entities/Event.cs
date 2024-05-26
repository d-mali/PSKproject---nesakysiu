using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

<<<<<<< HEAD:EventDomain/Entities/Event.cs
        public List<Participant>? Participants { get; set; }
=======
        public virtual ICollection<EventTask> Tasks { get; set; } = new List<EventTask>();
>>>>>>> d4ba34c0afd4c00ce362efca4531de031b30db4f:EventPlanningBackend/Entities/Event.cs
    }
}
