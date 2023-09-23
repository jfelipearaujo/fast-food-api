using Domain.Adapters.Models;

namespace Domain.Adapters
{
    public interface IClientRepository
    {
        Task<int> CreateAsync(ClientModel client, CancellationToken cancellationToken);

        Task<ClientModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<ClientModel?> GetByDocumentIdAsync(string documentId, CancellationToken cancellationToken);

        Task<ClientModel?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<IEnumerable<ClientModel>> GetAllAsync(CancellationToken cancellationToken);

        Task<int> UpdateAsync(ClientModel client, CancellationToken cancellationToken);

        Task<int> DeleteAsync(ClientModel client, CancellationToken cancellationToken);
    }
}
