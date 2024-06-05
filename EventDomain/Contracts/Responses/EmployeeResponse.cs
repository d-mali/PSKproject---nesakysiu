namespace EventDomain.Contracts.Responses
{
    public class EmployeeResponse
    {
        public string? Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
