using Domain.Common.Models;
using Domain.Entities.ClientAggregate.Errors;

using FluentResults;

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Domain.Entities.ClientAggregate.ValueObjects;

public partial class Email : ValueObject
{
    public string Value { get; private set; }

    [ExcludeFromCodeCoverage]
    private Email()
    {
    }

    private Email(string address)
    {
        Value = string.IsNullOrEmpty(address) ? string.Empty : address;
    }

    public static Result<Email> Create(string address)
    {
        var regex = EmailPattern();

        if (!string.IsNullOrEmpty(address) && !regex.IsMatch(address))
        {
            return Result.Fail(new EmailInvalidAddressError());
        }

        return new Email(address);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    [GeneratedRegex("^\\S+@\\S+\\.\\S+$")]
    private static partial Regex EmailPattern();
}

public static class EmailExtensions
{
    public static bool HasData(this Email valueObject)
    {
        return !string.IsNullOrEmpty(valueObject?.Value);
    }

    public static bool HasData(this Result<Email> valueObject)
    {
        return !string.IsNullOrEmpty(valueObject?.Value.Value);
    }
}