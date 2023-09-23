using FluentResults;

namespace Domain.Errors.ValueObjects.CPF
{
    public class CpfInvalidDocumentIdError : Error
    {
        public CpfInvalidDocumentIdError()
            : base("É esperado que seja informado um CPF válido")
        {
        }
    }
}
