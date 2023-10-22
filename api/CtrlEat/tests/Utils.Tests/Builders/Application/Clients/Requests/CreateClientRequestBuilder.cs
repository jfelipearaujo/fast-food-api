using Domain.UseCases.Clients.CreateClient.Requests;

namespace Utils.Tests.Builders.Application.Clients.Requests;

public class CreateClientRequestBuilder
{
    private string? firstName;
    private string? lastName;
    private string? email;
    private string? documentId;

    public CreateClientRequestBuilder()
    {
        Reset();
    }

    public CreateClientRequestBuilder Reset()
    {
        firstName = default;
        lastName = default;
        email = default;
        documentId = default;

        return this;
    }

    public CreateClientRequestBuilder WithSample()
    {
        WithFirstName("João");
        WithLastName("Silva");
        WithEmail("joao.silva@email.com");
        WithDocumentId("46808459029");

        return this;
    }

    public CreateClientRequestBuilder WithFirstName(string firstName)
    {
        this.firstName = firstName;
        return this;
    }

    public CreateClientRequestBuilder WithLastName(string lastName)
    {
        this.lastName = lastName;
        return this;
    }

    public CreateClientRequestBuilder WithEmail(string email)
    {
        this.email = email;
        return this;
    }

    public CreateClientRequestBuilder WithDocumentId(string documentId)
    {
        this.documentId = documentId;
        return this;
    }

    public CreateClientRequest Build()
    {
        return new CreateClientRequest
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            DocumentId = documentId,
        };
    }
}
