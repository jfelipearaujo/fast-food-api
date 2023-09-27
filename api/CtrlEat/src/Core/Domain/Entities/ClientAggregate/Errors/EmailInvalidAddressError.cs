using FluentResults;

namespace Domain.Entities.ClientAggregate.Errors;

public class EmailInvalidAddressError : Error
{
    public EmailInvalidAddressError()
        : base("O e-mail informado é inválido")
    {
    }
}
