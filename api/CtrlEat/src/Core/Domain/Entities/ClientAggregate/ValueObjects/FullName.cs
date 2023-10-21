using Domain.Common.Models;
using Domain.Entities.ClientAggregate.Errors;

using FluentResults;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.ClientAggregate.ValueObjects;

public sealed class FullName : ValueObject
{
    public const int MAX_LENGTH = 250;

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    [ExcludeFromCodeCoverage]
    private FullName()
    {
    }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<FullName> Create(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
        {
            return Result.Fail(new FullNameMissingFirstNameError());
        }

        if ((!string.IsNullOrEmpty(firstName) && firstName.Length > MAX_LENGTH)
            || (!string.IsNullOrEmpty(lastName) && lastName.Length > MAX_LENGTH))
        {
            return Result.Fail(new FullNameInvalidLengthError(MAX_LENGTH));
        }

        return new FullName(firstName, lastName);
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

    public static bool HasData(this Result<FullName> valueObject)
    {
        return !string.IsNullOrEmpty(valueObject?.Value.FirstName);
    }
}