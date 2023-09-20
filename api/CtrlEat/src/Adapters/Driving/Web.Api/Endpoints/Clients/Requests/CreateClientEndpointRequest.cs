namespace Web.Api.Endpoints.Clients.Requests
{
    public class CreateClientEndpointRequest
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? DocumentId { get; set; }
    }
}
