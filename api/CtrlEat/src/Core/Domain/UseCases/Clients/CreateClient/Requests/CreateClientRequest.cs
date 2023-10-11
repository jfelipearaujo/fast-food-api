namespace Domain.UseCases.Clients.CreateClient.Requests;

public class CreateClientRequest
{
    public string? FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; } = string.Empty;

    public string? Email { get; set; } = string.Empty;

    public string? DocumentId { get; set; } = string.Empty;
}
