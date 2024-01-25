using Domain.Entities.ClientAggregate.Enums;
using Web.Api.Endpoints.Clients;

namespace Utils.Tests.Builders.Api.ClientEndpoint.Responses;

public class ClientEndpointResponseBuilder
{
    private string? firstName;
    private string? lastName;
    private string? email;
    private string? documentId;
    private DocumentType documentType;
    private bool isAnonymous;

    public ClientEndpointResponseBuilder()
    {
        Reset();
    }

    public ClientEndpointResponseBuilder Reset()
    {
        firstName = default;
        lastName = default;
        email = default;
        documentId = default;
        documentType = default;
        isAnonymous = default;
        return this;
    }

    public ClientEndpointResponseBuilder FromRequest(CreateClientEndpointRequest request)
    {
        WithFirstName(request.FirstName);
        WithLastName(request.LastName);
        WithEmail(request.Email);
        WithDocumentId(request.DocumentId);

        return this;
    }

    public ClientEndpointResponseBuilder WithFirstName(string? firstName)
    {
        this.firstName = firstName;
        return this;
    }

    public ClientEndpointResponseBuilder WithLastName(string? lastName)
    {
        this.lastName = lastName;
        return this;
    }

    public ClientEndpointResponseBuilder WithEmail(string? email)
    {
        this.email = email;
        return this;
    }

    public ClientEndpointResponseBuilder WithDocumentId(string? documentId)
    {
        this.documentId = documentId;
        return this;
    }

    public ClientEndpointResponseBuilder WithDocumentType(DocumentType documentType)
    {
        this.documentType = documentType;
        return this;
    }

    public ClientEndpointResponseBuilder WithIsAnonymous(bool isAnonymous)
    {
        this.isAnonymous = isAnonymous;
        return this;
    }

    public ClientEndpointResponse Build()
    {
        return new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            DocumentId = documentId,
            DocumentType = documentType,
            IsAnonymous = isAnonymous,
        };
    }
}
