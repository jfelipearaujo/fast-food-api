using FluentResults;

namespace Domain.Errors.Clients
{
    public class ClientRegistrationInvalidDocumentIdError : Error
    {
        public ClientRegistrationInvalidDocumentIdError()
            : base("É esperado que seja informado um CPF válido")
        {
        }
    }
}
