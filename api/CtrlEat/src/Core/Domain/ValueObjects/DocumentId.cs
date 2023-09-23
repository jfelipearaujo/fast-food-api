using Domain.Abstract;
using Domain.Errors.ValueObjects.CPF;
using Domain.Validators;

using FluentResults;

namespace Domain.ValueObjects
{
    public class DocumentId : ValueObject, IValueObject<DocumentId>
    {
        public string Value { get; private set; }

        private DocumentId()
        {
        }

        public DocumentId(string documentId)
        {
            Value = string.IsNullOrEmpty(documentId) ? string.Empty : documentId;
        }

        public Result<DocumentId> Validate()
        {
            if (!string.IsNullOrEmpty(Value)
                && Value.Length > 0
                && !CpfValidator.Check(Value))
            {
                return Result.Fail(new CpfInvalidDocumentIdError());
            }

            return Result.Ok();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
