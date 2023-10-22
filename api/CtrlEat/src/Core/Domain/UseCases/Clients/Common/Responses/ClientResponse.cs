using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.Enums;

namespace Domain.UseCases.Clients.Common.Responses;

public class ClientResponse
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? DocumentId { get; set; }

    public DocumentType DocumentType { get; set; } = DocumentType.None;

    public bool IsAnonymous { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    // ---

    public static ClientResponse MapFromDomain(Client client)
    {
        return new ClientResponse
        {
            Id = client.Id.Value,
            FirstName = client.FullName.FirstName,
            LastName = client.FullName.LastName,
            Email = client.Email.Value,
            DocumentId = client.DocumentId.Value,
            DocumentType = client.DocumentType,
            IsAnonymous = client.IsAnonymous,
            CreatedAtUtc = client.CreatedAtUtc,
            UpdatedAtUtc = client.UpdatedAtUtc,
        };
    }

    public static IEnumerable<ClientResponse> MapFromDomain(IEnumerable<Client> clients)
    {
        return clients.Select(MapFromDomain);
    }
}
