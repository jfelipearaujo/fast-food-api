using Domain.Entities.ClientAggregate.Enums;
using Domain.UseCases.Clients.Common.Responses;

namespace Utils.Tests.Builders.Application.Clients.Responses;

public class ClientResponseBuilder
{
    private Guid id;
    private string? firstName;
    private string? lastName;
    private string? email;
    private string? documentId;
    private DocumentType documentType;
    private bool isAnonymous;

    public ClientResponseBuilder()
    {
        Reset();
    }

    public ClientResponseBuilder Reset()
    {
        id = default;
        firstName = default;
        lastName = default;
        email = default;
        documentId = default;
        documentType = default;
        isAnonymous = default;

        return this;
    }

    public ClientResponseBuilder WithSample()
    {
        WithId(Guid.NewGuid());
        WithFirstName("João");
        WithLastName("Silva");
        WithEmail("joao.silva@email.com");
        WithDocumentId("46808459029");
        WithDocumentType(DocumentType.CPF);
        WithIsAnonymous(false);

        return this;
    }

    public ClientResponseBuilder WithId(Guid id)
    {
        this.id = id;
        return this;
    }

    public ClientResponseBuilder WithFirstName(string firstName)
    {
        this.firstName = firstName;
        return this;
    }

    public ClientResponseBuilder WithLastName(string lastName)
    {
        this.lastName = lastName;
        return this;
    }

    public ClientResponseBuilder WithEmail(string email)
    {
        this.email = email;
        return this;
    }

    public ClientResponseBuilder WithDocumentId(string documentId)
    {
        this.documentId = documentId;
        return this;
    }

    public ClientResponseBuilder WithDocumentType(DocumentType documentType)
    {
        this.documentType = documentType;
        return this;
    }

    public ClientResponseBuilder WithIsAnonymous(bool isAnonymous)
    {
        this.isAnonymous = isAnonymous;
        return this;
    }

    public ClientResponse Build()
    {
        return new ClientResponse
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            DocumentId = documentId,
            DocumentType = documentType,
            IsAnonymous = isAnonymous,
        };
    }
}
