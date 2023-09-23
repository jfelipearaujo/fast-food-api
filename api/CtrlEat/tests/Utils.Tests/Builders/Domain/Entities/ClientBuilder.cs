using Domain.Entities.TypedIds;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ClientBuilder
    {
        private ClientId id;
        private string firstName;
        private string lastName;
        private string email;
        private string documentId;
        private bool isAnonymous;

        public ClientBuilder()
        {
            Reset();
        }

        public ClientBuilder Reset()
        {
            id = default;
            firstName = default;
            lastName = default;
            email = default;
            documentId = default;
            isAnonymous = default;

            return this;
        }

        public ClientBuilder WithSample()
        {
            id = new ClientId(Guid.NewGuid());
            firstName = "João";
            lastName = "Silva";
            email = "joao.silva@email.com";
            documentId = "46808459029";
            isAnonymous = false;

            return this;
        }

        public ClientBuilder WithId(Guid id)
        {
            this.id = new ClientId(id);

            return this;
        }

        public Client Build()
        {
            return new Client
            {
                Id = id,
                FullName = FullName.Create(firstName, lastName).ValueOrDefault,
                Email = Email.Create(email).ValueOrDefault,
                PersonalDocument = Cpf.Create(documentId).ValueOrDefault,
                IsAnonymous = isAnonymous,
            };
        }
    }
}