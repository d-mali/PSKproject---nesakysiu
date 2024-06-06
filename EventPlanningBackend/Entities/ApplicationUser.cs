using EventDomain.Contracts.Responses;
using EventDomain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventBackend.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public UserPrivilegeType Privilege { get; set; }

        [Timestamp]
        public byte[]? Version { get; set; }

        public virtual List<Event>? Events { get; set; }

        public virtual ICollection<EventTask> Tasks { get; set; } = new List<EventTask>();

        public EmployeeResponse ToResponse()
        {
            return new EmployeeResponse
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName
            };
        }
    }
}
