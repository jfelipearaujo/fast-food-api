using FluentResults;

namespace Application.UseCases.Clients.CreateClient.Errors;

public class ClientRegistrationWithoutEmailError : Error
{
    public ClientRegistrationWithoutEmailError()
        : base("É esperado que seja informado o endereço de e-mail")
    {
    }
}
