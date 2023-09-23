using Domain.Adapters;
using Domain.Entities;
using Domain.Entities.StrongIds;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateAsync(Product product, CancellationToken cancellationToken)
        {
            context.Product.Add(product);

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(Product product, CancellationToken cancellationToken)
        {
            context.Product.Remove(product);

            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.Product
                .Include(x => x.ProductCategory)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllByCategoryAsync(string category, CancellationToken cancellationToken)
        {
            return await context.Product
                .Include(x => x.ProductCategory)
                .Where(x => x.ProductCategory.Description == category)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken)
        {
            return await context.Product.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<int> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            context.Product.Update(product);

            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
