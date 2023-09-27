using Domain.Common.Models;
using Domain.Entities.ClientAggregate.Enums;
using Domain.Entities.ClientAggregate.ValueObjects;

using FluentResults;

namespace Domain.Entities.ClientAggregate;

public sealed class Client : AggregateRoot<ClientId>
{
    public FullName FullName { get; private set; }

    public Email Email { get; private set; }

    public DocumentId DocumentId { get; private set; }

    public DocumentType DocumentType { get; private set; }

    public bool IsAnonymous { get; private set; }

    private Client()
    {
    }

    private Client(
        FullName fullName,
        Email email,
        DocumentId documentId,
        bool isAnonymous,
        DocumentType? documentType = null,
        ClientId? clientId = null)
        : base(clientId ?? ClientId.CreateUnique())
    {
        FullName = fullName;
        Email = email;
        DocumentId = documentId;
        DocumentType = documentType ?? DocumentType.None;
        IsAnonymous = isAnonymous;
    }

    public void Update(Email email)
    {
        Email = email;
    }

    public static Result<Client> Create(
        string firstName,
        string lastName,
        string email,
        string documentId,
        ClientId? clientId = null)
    {
        var fullNameValueObject = FullName.Create(firstName, lastName);
        var documentIdValueObject = DocumentId.Create(documentId);
        var emailValueObject = Email.Create(email);

        var fullNameValidation = fullNameValueObject.Validate();
        var documentIdValidation = documentIdValueObject.Validate();
        var emailValidation = emailValueObject.Validate();

        if (fullNameValidation.IsFailed)
        {
            return Result.Fail(fullNameValidation.Errors);
        }

        if (documentIdValidation.IsFailed)
        {
            return Result.Fail(documentIdValidation.Errors);
        }

        if (emailValidation.IsFailed)
        {
            return Result.Fail(emailValidation.Errors);
        }

        var isAnonymous = !fullNameValueObject.HasData()
            && !emailValueObject.HasData()
            && !documentIdValueObject.HasData();

        return new Client(
            fullNameValueObject,
            emailValueObject,
            documentIdValueObject,
            isAnonymous,
            isAnonymous ? DocumentType.None : DocumentType.CPF,
            clientId
        );
    }
}
