using System.ComponentModel.DataAnnotations;

namespace EventDomain.Contracts.Requests
{
    public class EmployeeRequest
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

    }
}
