using FluentResults;

namespace Domain.Entities.ClientAggregate.Errors;

public class FullNameMissingFirstNameError : Error
{
    public FullNameMissingFirstNameError()
        : base("É esperado que seja informado o primeiro nome quando o sobrenome for informado")
    {
    }
}