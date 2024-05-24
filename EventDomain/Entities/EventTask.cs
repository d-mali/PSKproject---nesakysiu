using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventDomain.Entities
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

        public string? Description { get; set; }
    }
}
