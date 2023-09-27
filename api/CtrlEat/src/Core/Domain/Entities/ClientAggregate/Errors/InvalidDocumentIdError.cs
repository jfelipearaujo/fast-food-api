using FluentResults;

namespace Domain.Entities.ClientAggregate.Errors;

public class InvalidDocumentIdError : Error
{
    public InvalidDocumentIdError()
        : base("É esperado que seja informado um CPF válido")
    {
    }
}
