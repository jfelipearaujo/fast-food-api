using FluentResults;

namespace Domain.Errors.Clients
{
    public class ClientRegistrationEmailAlreadyExistsError : Error
    {
        public ClientRegistrationEmailAlreadyExistsError()
            : base("O endereço de e-mail informado já está sendo utilizado por outra pessoa")
        {
        }
    }
}
