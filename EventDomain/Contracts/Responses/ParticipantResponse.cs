namespace EventDomain.Contracts.Responses
{
    public class ParticipantResponse
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateOnly BirthDate { get; set; }

        public required string Email { get; set; }
    }
}
