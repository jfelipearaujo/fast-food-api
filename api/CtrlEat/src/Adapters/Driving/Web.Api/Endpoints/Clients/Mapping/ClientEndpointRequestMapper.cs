using Domain.UseCases.Clients.CreateClient.Requests;

namespace Web.Api.Endpoints.Clients.Mapping;

public static class ClientEndpointRequestMapper
{
    public static CreateClientRequest MapToRequest(this CreateClientEndpointRequest request)
    {
        return new CreateClientRequest
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DocumentId = request.DocumentId,
            Email = request.Email
        };
    }
}
