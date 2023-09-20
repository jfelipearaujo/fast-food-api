using FluentResults;

namespace Domain.Errors.Clients
{
    public class ClientRegistrationDocumentIdAlreadyExistsError : Error
    {
        public ClientRegistrationDocumentIdAlreadyExistsError()
            : base("O CPF informado já está cadastrado")
        {
        }
    }

    public class ClientNotFoundError : Error
    {
        public ClientNotFoundError(Guid id)
            : base($"O cliente com o identificador '{id}' não foi encontrado")
        {
        }
    }
}
