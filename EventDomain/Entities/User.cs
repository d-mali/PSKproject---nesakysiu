using EventDomain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventDomain.Entities
{
    public class User
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public UserPrivilegeType Privilege { get; set; }
    }
}
