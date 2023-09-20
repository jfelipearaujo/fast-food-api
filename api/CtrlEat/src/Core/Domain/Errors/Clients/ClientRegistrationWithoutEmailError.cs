using FluentResults;

namespace Domain.Errors.Clients
{
    public class ClientRegistrationWithoutEmailError : Error
    {
        public ClientRegistrationWithoutEmailError()
            : base("É esperado que seja informado o endereço de e-mail")
        {
        }
    }
}
