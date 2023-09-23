using Domain.Abstract;
using Domain.Entities.StrongIds;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Client : Entity<ClientId>
    {
        public Name FirstName { get; set; }

        public Name LastName { get; set; }

        public Email Email { get; set; }

        public DocumentId DocumentId { get; set; }

        public DocumentType DocumentType { get; set; }

        public bool IsAnonymous { get; set; }

    }
}
