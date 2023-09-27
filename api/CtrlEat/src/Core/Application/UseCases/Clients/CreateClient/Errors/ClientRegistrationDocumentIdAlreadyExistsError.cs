using FluentResults;

namespace Application.UseCases.Clients.CreateClient.Errors;

public class ClientRegistrationDocumentIdAlreadyExistsError : Error
{
    public ClientRegistrationDocumentIdAlreadyExistsError()
        : base("O CPF informado já está cadastrado")
    {
    }
}
