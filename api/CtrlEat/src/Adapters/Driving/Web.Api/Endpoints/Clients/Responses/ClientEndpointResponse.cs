using Domain.Enums;

using System.Text.Json.Serialization;

namespace Web.Api.Endpoints.Clients.Responses
{
    public class ClientEndpointResponse
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? DocumentId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DocumentType DocumentType { get; set; }

        public bool IsAnonymous { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }
    }
}
