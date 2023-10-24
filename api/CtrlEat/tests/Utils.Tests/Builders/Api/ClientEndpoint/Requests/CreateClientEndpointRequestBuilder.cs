using Bogus;
using Bogus.Extensions.Brazil;

using Web.Api.Endpoints.Clients.Requests;

namespace Utils.Tests.Builders.Api.ClientEndpoint.Requests;

public class CreateClientEndpointRequestBuilder
{
    private readonly Faker faker;

    private string? firstName;
    private string? lastName;
    private string? email;
    private string? documentId;

    public CreateClientEndpointRequestBuilder()
    {
        faker = new Faker("pt_BR");

        Reset();
    }

    public CreateClientEndpointRequestBuilder Reset()
    {
        firstName = default;
        lastName = default;
        email = default;
        documentId = default;

        return this;
    }

    public CreateClientEndpointRequestBuilder WithSample()
    {
        WithFirstName(faker.Person.FirstName);
        WithLastName(faker.Person.LastName);
        WithEmail(faker.Person.Email);
        WithDocumentId(faker.Person.Cpf());

        return this;
    }

    public CreateClientEndpointRequestBuilder WithEmptySample()
    {
        WithFirstName(string.Empty);
        WithLastName(string.Empty);
        WithEmail(string.Empty);
        WithDocumentId(string.Empty);

        return this;
    }

    public CreateClientEndpointRequestBuilder WithFirstName(string firstName)
    {
        this.firstName = firstName;
        return this;
    }

    public CreateClientEndpointRequestBuilder WithLastName(string lastName)
    {
        this.lastName = lastName;
        return this;
    }

    public CreateClientEndpointRequestBuilder WithEmail(string email)
    {
        this.email = email;
        return this;
    }

    public CreateClientEndpointRequestBuilder WithDocumentId(string documentId)
    {
        this.documentId = documentId;
        return this;
    }

    public CreateClientEndpointRequest Build()
    {
        return new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            DocumentId = documentId
        };
    }
}
