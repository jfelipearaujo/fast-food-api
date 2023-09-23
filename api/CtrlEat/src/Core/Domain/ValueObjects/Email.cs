using Domain.Abstract;
using Domain.Errors.ValueObjects.Email;

using FluentResults;

using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial class Email : ValueObject
    {
        public string Address { get; private set; }

        private Email(string address)
        {
            Address = address;
        }

        public static Result<Email> Create(string address)
        {
            if (!string.IsNullOrEmpty(address) && !EmailAddressRegex().IsMatch(address))
            {
                return Result.Fail(new EmailInvalidAddressError());
            }

            return new Email(address);
        }

        [GeneratedRegex("^\\S+@\\S+\\.\\S+$")]
        private static partial Regex EmailAddressRegex();

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address;
        }
    }

    public static class EmailExtensions
    {
        public static bool HasData(this Result<Email> result)
        {
            if (result.IsFailed)
            {
                return false;
            }

            return !string.IsNullOrEmpty(result.Value.Address);
        }
    }
}
