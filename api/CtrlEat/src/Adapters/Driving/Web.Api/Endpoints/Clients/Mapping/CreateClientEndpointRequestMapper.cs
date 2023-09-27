using Domain.UseCases.Clients.Requests;

using Web.Api.Endpoints.Clients.Requests;

namespace Web.Api.Endpoints.Clients.Mapping
{
    public static class CreateClientEndpointRequestMapper
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
}
