using Domain.Abstract;
using Domain.Errors.ValueObjects.Name;

using FluentResults;

namespace Domain.ValueObjects
{
    public class FullName : ValueObject
    {
        public string? FirstName { get; private set; }

        public string? LastName { get; private set; }

        private FullName(string? firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<FullName> Create(string? firstName, string? lastName)
        {
            if (string.IsNullOrEmpty(firstName) &&
                !string.IsNullOrEmpty(lastName))
            {
                return Result.Fail(new NameInvalidFirstNameMissingError());
            }

            return new FullName(firstName, lastName);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }

    public static class FullNameExtensions
    {
        public static bool HasData(this Result<FullName> result)
        {
            if (result.IsFailed)
            {
                return false;
            }

            return !string.IsNullOrEmpty(result.Value.FirstName)
                    && !string.IsNullOrEmpty(result.Value.LastName);
        }
    }
}
