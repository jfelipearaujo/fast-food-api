using Domain.Adapters;
using Domain.Entities;

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

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.Product.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
