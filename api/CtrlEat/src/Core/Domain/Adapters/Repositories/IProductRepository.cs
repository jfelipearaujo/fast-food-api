using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;

namespace Domain.Adapters.Repositories;

public interface IProductRepository
{
    Task<int> CreateAsync(Product product, CancellationToken cancellationToken);

    Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken);

    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);

    Task<IEnumerable<Product>> GetAllByCategoryAsync(string category, CancellationToken cancellationToken);

    Task<int> UpdateAsync(Product product, CancellationToken cancellationToken);

    Task<int> DeleteAsync(Product product, CancellationToken cancellationToken);
}
