using Domain.Entities;

namespace Domain.Adapters
{
    public interface IProductRepository
    {
        Task<int> CreateAsync(Product product, CancellationToken cancellationToken);

        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
