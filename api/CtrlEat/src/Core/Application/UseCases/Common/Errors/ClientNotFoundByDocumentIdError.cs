using FluentResults;

namespace Application.UseCases.Common.Errors;

public class ClientNotFoundByDocumentIdError : Error
{
    public ClientNotFoundByDocumentIdError(string documentId)
        : base($"O cliente com o CPF '{documentId}' não foi encontrado")
    {
    }
}
