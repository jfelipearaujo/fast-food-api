using FluentResults;

namespace Domain.Errors.ValueObjects.Name
{
    public class NameInvalidLengthError : Error
    {
        public NameInvalidLengthError(int maxLength)
            : base($"O nome ou o sobrenome não podem ter mais do que ${maxLength} carecteres")
        {
        }
    }
}
