using Domain.Common.Models;
using Domain.Entities.ClientAggregate.Errors;

using FluentResults;

using System.Text.RegularExpressions;

namespace Domain.Entities.ClientAggregate.ValueObjects;

public partial class Email : ValueObject, IValidateValueObject<Email>
{
    public string Value { get; private set; }

    private Email()
    {
    }

    private Email(string address)
    {
        Value = string.IsNullOrEmpty(address) ? string.Empty : address;
    }

    public static Email Create(string address)
    {
        return new Email(address);
    }

    public Result<Email> Validate()
    {
        var regex = new Regex("^\\S+@\\S+\\.\\S+$");

        if (!string.IsNullOrEmpty(Value) && !regex.IsMatch(Value))
        {
            return Result.Fail(new EmailInvalidAddressError());
        }

        return Result.Ok();
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

public static class EmailExtensions
{
    public static bool HasData(this Email valueObject)
    {
        return !string.IsNullOrEmpty(valueObject?.Value);
    }
}