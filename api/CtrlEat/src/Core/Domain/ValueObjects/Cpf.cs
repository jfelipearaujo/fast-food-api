using Domain.Abstract;
using Domain.Enums;
using Domain.Errors.ValueObjects.CPF;
using Domain.Validators;

using FluentResults;

namespace Domain.ValueObjects
{
    public class Cpf : ValueObject
    {
        public string? DocumentId { get; private set; }

        public DocumentType DocumentType { get; private set; }

        private Cpf(string? documentId, DocumentType documentType)
        {
            DocumentId = documentId;
            DocumentType = documentType;
        }

        public static Result<Cpf> Create(string? documentId)
        {
            if (!string.IsNullOrEmpty(documentId) && !CpfValidator.Check(documentId))
            {
                return Result.Fail(new CpfInvalidDocumentIdError());
            }

            return new Cpf(documentId,
                string.IsNullOrEmpty(documentId)
                ? DocumentType.None
                : DocumentType.CPF);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DocumentId;
            yield return DocumentType;
        }
    }

    public static class CpfExtensions
    {
        public static bool HasData(this Result<Cpf> result)
        {
            if (result.IsFailed)
            {
                return false;
            }

            return !string.IsNullOrEmpty(result.Value.DocumentId);
        }
    }
}
