using Domain.Common.Models;
using Domain.Entities.ClientAggregate.Enums;
using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.Entities.OrderAggregate;

using FluentResults;

namespace Domain.Entities.ClientAggregate;

public sealed class Client : AggregateRoot<ClientId>
{
    public FullName FullName { get; private set; }

    public Email Email { get; private set; }

    public DocumentId DocumentId { get; private set; }

    public DocumentType DocumentType { get; private set; }

    public bool IsAnonymous { get; private set; }

    public ICollection<Order> Orders { get; set; }

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
        string emailAddress,
        string documentIdNumber,
        ClientId? clientId = null)
    {
        var fullName = FullName.Create(firstName, lastName);
        var documentId = DocumentId.Create(documentIdNumber);
        var email = Email.Create(emailAddress);

        if (fullName.IsFailed)
        {
            return Result.Fail(fullName.Errors);
        }

        if (documentId.IsFailed)
        {
            return Result.Fail(documentId.Errors);
        }

        if (email.IsFailed)
        {
            return Result.Fail(email.Errors);
        }

        var isAnonymous = !fullName.HasData()
            && !email.HasData()
            && !documentId.HasData();

        return new Client(
            fullName.Value,
            email.Value,
            documentId.Value,
            isAnonymous,
            isAnonymous ? DocumentType.None : DocumentType.CPF,
            clientId
        );
    }
}
