namespace Domain.UseCases.Clients.Requests
{
    public class CreateClientRequest
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? DocumentId { get; set; }
    }
}
