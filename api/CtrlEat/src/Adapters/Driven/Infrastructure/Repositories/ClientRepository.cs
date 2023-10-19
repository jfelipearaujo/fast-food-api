using Domain.Adapters.Repositories;
using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext context;

    public ClientRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<int> CreateAsync(Client client, CancellationToken cancellationToken)
    {
        context.Client.Add(client);

        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Client client, CancellationToken cancellationToken)
    {
        context.Client.Remove(client);

        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Client.ToListAsync(cancellationToken);
    }

    public async Task<Client?> GetByDocumentIdAsync(DocumentId documentId, CancellationToken cancellationToken)
    {
        return await context.Client.FirstOrDefaultAsync(x => x.DocumentId == documentId, cancellationToken);
    }

    public async Task<Client?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await context.Client.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<Client?> GetByIdAsync(ClientId id, CancellationToken cancellationToken)
    {
        return await context.Client.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<int> UpdateAsync(Client client, CancellationToken cancellationToken)
    {
        context.Client.Update(client);

        return await context.SaveChangesAsync(cancellationToken);
    }
}
