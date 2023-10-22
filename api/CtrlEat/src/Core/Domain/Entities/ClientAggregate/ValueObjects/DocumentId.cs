using Domain.Common.Models;
using Domain.Entities.ClientAggregate.Errors;
using Domain.Entities.ClientAggregate.Validators;

using FluentResults;

using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.ClientAggregate.ValueObjects;

public sealed class DocumentId : ValueObject
{
    public string Value { get; private set; }

    [ExcludeFromCodeCoverage]
    private DocumentId()
    {
    }

    private DocumentId(string documentId)
    {
        Value = documentId;
    }

    public static Result<DocumentId> Create(string documentId)
    {
        if (!string.IsNullOrEmpty(documentId)
            && documentId.Length > 0
            && !CpfValidator.Check(documentId))
        {
            return Result.Fail(new InvalidDocumentIdError());
        }

        return new DocumentId(documentId);
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

    public static bool HasData(this Result<DocumentId> valueObject)
    {
        return !string.IsNullOrEmpty(valueObject?.Value.Value);
    }
}