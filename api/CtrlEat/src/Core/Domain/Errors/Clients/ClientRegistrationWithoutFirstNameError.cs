using FluentResults;

namespace Domain.Errors.Clients
{
    public class ClientRegistrationWithoutFirstNameError : Error
    {
        public ClientRegistrationWithoutFirstNameError()
            : base("É esperado que seja informado o primeiro nome")
        {
        }
    }
}
