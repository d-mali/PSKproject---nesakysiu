using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventDomain.Entities
{
    public class EventTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public DateTime? ScheduledTime { get; set; }

        public string? Description { get; set; }
    }
}
