using Domain.Common.Models;
using Domain.Entities.ClientAggregate.Errors;

using FluentResults;

namespace Domain.Entities.ClientAggregate.ValueObjects;

public sealed class FullName : ValueObject, IValidateValueObject<FullName>
{
    private const int MAX_LENGTH = 250;

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    private FullName()
    {

    }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static FullName Create(string firstName, string lastName)
    {
        return new FullName(firstName, lastName);
    }

    public Result<FullName> Validate()
    {
        if (!string.IsNullOrEmpty(FirstName) && FirstName.Length > MAX_LENGTH)
        {
            return Result.Fail(new FullNameInvalidLengthError(MAX_LENGTH));
        }

        if (!string.IsNullOrEmpty(LastName) && LastName.Length > MAX_LENGTH)
        {
            return Result.Fail(new FullNameInvalidLengthError(MAX_LENGTH));
        }

        return Result.Ok();
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}

public static class FullNameExtensions
{
    public static bool HasData(this FullName valueObject)
    {
        return !string.IsNullOrEmpty(valueObject?.FirstName);
    }
}