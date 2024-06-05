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

        public List<Event>? Events { get; set; }

        public List<EventTask>? Tasks { get; set; }
    }
}
