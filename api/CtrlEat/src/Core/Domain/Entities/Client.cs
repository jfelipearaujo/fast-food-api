using Domain.Abstract;
using Domain.Enums;

namespace Domain.Entities
{
    public class Client : Entity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? DocumentId { get; set; }

        public DocumentType DocumentType { get; set; } = DocumentType.None;

        public bool IsAnonymous { get; set; }
    }
}
