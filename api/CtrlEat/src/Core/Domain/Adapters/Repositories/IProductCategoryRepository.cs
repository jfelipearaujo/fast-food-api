using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;

namespace Domain.Adapters.Repositories;

public interface IProductCategoryRepository
{
    Task<int> CreateAsync(ProductCategory productCategory, CancellationToken cancellationToken);

    Task<ProductCategory?> GetByIdAsync(ProductCategoryId id, CancellationToken cancellationToken);

    Task<IEnumerable<ProductCategory>> GetAllAsync(CancellationToken cancellationToken);

    Task<int> UpdateAsync(ProductCategory productCategory, CancellationToken cancellationToken);

    Task<int> DeleteAsync(ProductCategory productCategory, CancellationToken cancellationToken);
}