using FluentResults;

namespace Application.UseCases.Clients.CreateClient.Errors;

public class ClientRegistrationEmailAlreadyExistsError : Error
{
    public ClientRegistrationEmailAlreadyExistsError()
        : base("O endereço de e-mail informado já está sendo utilizado por outra pessoa")
    {
    }
}
