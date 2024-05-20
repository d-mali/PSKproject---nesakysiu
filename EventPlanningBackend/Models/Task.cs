using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanningBackend.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
    
    }
}
