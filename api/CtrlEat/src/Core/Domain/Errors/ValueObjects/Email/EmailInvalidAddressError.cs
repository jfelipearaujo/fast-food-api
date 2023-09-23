using FluentResults;

namespace Domain.Errors.ValueObjects.Email
{
    public class EmailInvalidAddressError : Error
    {
        public EmailInvalidAddressError()
            : base("O e-mail informado é inválido")
        {
        }
    }
}
