using FluentResults;

namespace Application.UseCases.Common.Errors;

public class ClientNotFoundError : Error
{
    public ClientNotFoundError(Guid id)
        : base($"O cliente com o identificador '{id}' não foi encontrado")
    {
    }
}
