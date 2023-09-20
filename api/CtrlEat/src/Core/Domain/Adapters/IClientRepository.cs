using Domain.Entities;

namespace Domain.Adapters
{
    public interface IClientRepository
    {
        Task<int> CreateAsync(Client client, CancellationToken cancellationToken);

        Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Client?> GetByDocumentIdAsync(string documentId, CancellationToken cancellationToken);

        Task<Client?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken);

        Task<int> UpdateAsync(Client client, CancellationToken cancellationToken);

        Task<int> DeleteAsync(Client client, CancellationToken cancellationToken);
    }
}
