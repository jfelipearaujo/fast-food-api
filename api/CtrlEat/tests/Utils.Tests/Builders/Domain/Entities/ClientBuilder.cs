using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.ValueObjects;

namespace Utils.Tests.Builders.Domain.Entities;

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
        id = ClientId.CreateUnique();
        firstName = "João";
        lastName = "Silva";
        email = "joao.silva@email.com";
        documentId = "46808459029";
        isAnonymous = false;

        return this;
    }

    public ClientBuilder WithId(Guid id)
    {
        this.id = ClientId.Create(id);

        return this;
    }

    public Client Build()
    {
        return Client.Create(
            firstName,
            lastName,
            email,
            documentId,
            id).ValueOrDefault;
    }
}