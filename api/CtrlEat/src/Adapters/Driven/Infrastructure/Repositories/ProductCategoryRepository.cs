using Domain.Adapters;
using Domain.Models;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Infrastructure.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductCategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(ProductCategory productCategory, CancellationToken cancellationToken)
        {
            _dbContext.ProductCategory.Add(productCategory);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var productCategory = await _dbContext.ProductCategory.FirstAsync(x => x.Id == id, cancellationToken);

            _dbContext.ProductCategory.Remove(productCategory);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.ProductCategory.ToListAsync(cancellationToken);
        }

        public async Task<ProductCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.ProductCategory.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<int> UpdateAsync(ProductCategory productCategory, CancellationToken cancellationToken)
        {
            _dbContext.ProductCategory.Update(productCategory);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}