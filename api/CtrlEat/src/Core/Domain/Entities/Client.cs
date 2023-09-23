using Domain.Abstract;
using Domain.Entities.TypedIds;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Client : Entity<ClientId>
    {
        public FullName? FullName { get; set; }

        public Email? Email { get; set; }

        public Cpf? PersonalDocument { get; set; }

        public bool IsAnonymous { get; set; }
    }
}
