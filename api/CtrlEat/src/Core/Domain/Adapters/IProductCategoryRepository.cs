using Domain.Entities;
using Domain.Entities.TypedIds;

namespace Domain.Adapters
{
    public interface IProductCategoryRepository
    {
        Task<int> CreateAsync(ProductCategory productCategory, CancellationToken cancellationToken);

        Task<ProductCategory?> GetByIdAsync(ProductCategoryId id, CancellationToken cancellationToken);

        Task<IEnumerable<ProductCategory>> GetAllAsync(CancellationToken cancellationToken);

        Task<int> UpdateAsync(ProductCategory productCategory, CancellationToken cancellationToken);

        Task<int> DeleteAsync(ProductCategory productCategory, CancellationToken cancellationToken);
    }
}