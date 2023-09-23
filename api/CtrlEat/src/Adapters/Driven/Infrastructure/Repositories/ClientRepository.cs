using Domain.Adapters;
using Domain.Adapters.Models;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext context;

        public ClientRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateAsync(ClientModel client, CancellationToken cancellationToken)
        {
            context.Client.Add(client);

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(ClientModel client, CancellationToken cancellationToken)
        {
            context.Client.Remove(client);

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ClientModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.Client.ToListAsync(cancellationToken);
        }

        public async Task<ClientModel?> GetByDocumentIdAsync(string documentId, CancellationToken cancellationToken)
        {
            return await context.Client.FirstOrDefaultAsync(x => x.DocumentId == documentId, cancellationToken);
        }

        public async Task<ClientModel?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await context.Client.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<ClientModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.Client.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<int> UpdateAsync(ClientModel client, CancellationToken cancellationToken)
        {
            context.Client.Update(client);

            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
