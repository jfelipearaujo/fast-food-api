using Domain.Adapters;
using Domain.Entities;
using Domain.Entities.TypedIds;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Infrastructure.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AppDbContext context;

        public ProductCategoryRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateAsync(ProductCategory productCategory, CancellationToken cancellationToken)
        {
            context.ProductCategory.Add(productCategory);

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(ProductCategory productCategory, CancellationToken cancellationToken)
        {
            context.ProductCategory.Remove(productCategory);

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.ProductCategory.ToListAsync(cancellationToken);
        }

        public async Task<ProductCategory?> GetByIdAsync(ProductCategoryId id, CancellationToken cancellationToken)
        {
            return await context.ProductCategory.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<int> UpdateAsync(ProductCategory productCategory, CancellationToken cancellationToken)
        {
            context.ProductCategory.Update(productCategory);

            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}