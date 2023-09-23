using Domain.Abstract;
using Domain.Errors.ValueObjects.Email;

using FluentResults;

using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial class Email : ValueObject, IValueObject<Email>
    {
        public string Value { get; private set; }

        private Email()
        {
        }

        public Email(string address)
        {
            Value = string.IsNullOrEmpty(address) ? string.Empty : address;
        }

        public Result<Email> Validate()
        {
            if (!string.IsNullOrEmpty(Value) && !EmailAddressRegex().IsMatch(Value))
            {
                return Result.Fail(new EmailInvalidAddressError());
            }

            return Result.Ok();
        }

        [GeneratedRegex("^\\S+@\\S+\\.\\S+$")]
        private static partial Regex EmailAddressRegex();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
