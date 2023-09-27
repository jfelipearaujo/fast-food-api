using Domain.Common.Models;
using Domain.Entities.ClientAggregate.Errors;
using Domain.Entities.ClientAggregate.Validators;

using FluentResults;

namespace Domain.Entities.ClientAggregate.ValueObjects;

public sealed class DocumentId : ValueObject, IValidateValueObject<DocumentId>
{
    public string Value { get; private set; }

    private DocumentId()
    {
    }

    private DocumentId(string documentId)
    {
        Value = string.IsNullOrEmpty(documentId) ? string.Empty : documentId;
    }

    public static DocumentId Create(string documentId)
    {
        return new DocumentId(documentId);
    }

    public Result<DocumentId> Validate()
    {
        if (!string.IsNullOrEmpty(Value)
            && Value.Length > 0
            && !CpfValidator.Check(Value))
        {
            return Result.Fail(new InvalidDocumentIdError());
        }

        return Result.Ok();
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

public static class DocumentIdExtensions
{
    public static bool HasData(this DocumentId valueObject)
    {
        return !string.IsNullOrEmpty(valueObject?.Value);
    }
}