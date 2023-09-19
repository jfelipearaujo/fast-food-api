using Domain.Entities;

namespace Domain.Adapters
{
    public interface IProductRepository
    {
        Task<int> CreateAsync(Product product, CancellationToken cancellationToken);

        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);

        Task<int> UpdateAsync(Product product, CancellationToken cancellationToken);

        Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
