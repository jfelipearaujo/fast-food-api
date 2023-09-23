namespace Domain.Adapters.Models
{
    public class ClientModel : IBaseModel
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? DocumentId { get; set; }

        public int DocumentType { get; set; }

        public bool IsAnonymous { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }
    }
}
