using Domain.Abstract;
using Domain.Enums;
using Domain.Errors.Clients;
using Domain.ValueObjects;
using Domain.ValueObjects.Extensions;

using FluentResults;

namespace Domain.Entities
{
    public class Client : Entity<Guid>
    {
        public Name FirstName { get; set; }

        public Name LastName { get; set; }

        public Email Email { get; set; }

        public DocumentId DocumentId { get; set; }

        public DocumentType DocumentType { get; set; }

        public bool IsAnonymous { get; set; }

        public static Result<Client> ValidateAndCreate(
            string firstName,
            string lastName,
            string email,
            string documentId)
        {
            var firstNameValueObject = new Name(firstName);
            var lastNameValueObject = new Name(lastName);
            var documentIdValueObject = new DocumentId(documentId);
            var emailValueObject = new Email(email);

            var firstNameValidation = firstNameValueObject.Validate();
            var lastNameValidation = lastNameValueObject.Validate();
            var documentIdValidation = documentIdValueObject.Validate();
            var emailValidation = emailValueObject.Validate();

            if (firstNameValidation.IsFailed)
            {
                return Result.Fail(firstNameValidation.Errors);
            }

            if (lastNameValidation.IsFailed)
            {
                return Result.Fail(lastNameValidation.Errors);
            }

            if (documentIdValidation.IsFailed)
            {
                return Result.Fail(documentIdValidation.Errors);
            }

            if (emailValidation.IsFailed)
            {
                return Result.Fail(emailValidation.Errors);
            }

            if (firstNameValueObject.HasData() && !emailValueObject.HasData())
            {
                return Result.Fail(new ClientRegistrationWithoutEmailError());
            }

            var isAnonymous = !firstNameValueObject.HasData()
                && !emailValueObject.HasData()
                && !documentIdValueObject.HasData();

            return new Client
            {
                Id = Guid.NewGuid(),
                FirstName = firstNameValueObject,
                LastName = lastNameValueObject,
                Email = emailValueObject,
                DocumentId = documentIdValueObject,
                DocumentType = isAnonymous ? DocumentType.None : DocumentType.CPF,
                IsAnonymous = isAnonymous
            };
        }
    }
}
