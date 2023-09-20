using Domain.Enums;

namespace Domain.UseCases.Clients.Responses
{
    public class ClientResponse
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? DocumentId { get; set; }

        public DocumentType DocumentType { get; set; } = DocumentType.None;

        public bool IsAnonymous { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }
    }
}
