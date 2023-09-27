using FluentResults;

namespace Application.UseCases.Clients.GetClientById.Errors;

public class ClientNotFoundError : Error
{
    public ClientNotFoundError(Guid id)
        : base($"O cliente com o identificador '{id}' não foi encontrado")
    {
    }
}
