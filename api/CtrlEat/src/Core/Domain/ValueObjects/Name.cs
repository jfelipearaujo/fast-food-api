using Domain.Abstract;
using Domain.Errors.ValueObjects.Name;

using FluentResults;

namespace Domain.ValueObjects
{
    public class Name : ValueObject, IValueObject<Name>
    {
        private const int MAX_LENGTH = 250;

        public string Value { get; private set; }

        private Name()
        {
        }

        public Name(string name)
        {
            Value = string.IsNullOrEmpty(name) ? string.Empty : name;
        }

        public Result<Name> Validate()
        {
            if (!string.IsNullOrEmpty(Value) && Value.Length > MAX_LENGTH)
            {
                return Result.Fail(new NameInvalidLengthError(MAX_LENGTH));
            }

            return Result.Ok();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
