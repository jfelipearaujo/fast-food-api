using FluentResults;

namespace Domain.Entities.ClientAggregate.Errors;

public class FullNameInvalidLengthError : Error
{
    public FullNameInvalidLengthError(int maxLength)
        : base($"O nome e o sobrenome não podem ter mais do que {maxLength} carecteres")
    {
    }
}
