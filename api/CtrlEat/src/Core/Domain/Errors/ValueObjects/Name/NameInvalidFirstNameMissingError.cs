using FluentResults;

namespace Domain.Errors.ValueObjects.Name
{
    public class NameInvalidFirstNameMissingError : Error
    {
        public NameInvalidFirstNameMissingError()
            : base("É necessário informar o primeiro nome")
        {
        }
    }
}
