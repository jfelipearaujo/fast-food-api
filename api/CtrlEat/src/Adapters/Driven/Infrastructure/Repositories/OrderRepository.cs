using Domain.Adapters;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext context;

    public OrderRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<int> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        context.Order.Add(order);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Order order, CancellationToken cancellationToken)
    {
        context.Order.Remove(order);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Order.ToListAsync(cancellationToken);
    }

    public async Task<Dictionary<OrderStatus, List<Order>>> GetAllByStatusAsync(OrderStatus status, CancellationToken cancellationToken)
    {
        var query = context.Order.AsQueryable();

        if (status == OrderStatus.None)
        {
            query.Where(x => x.Status != OrderStatus.Completed
            || (x.Status == OrderStatus.Completed && x.StatusUpdatedAt >= DateTime.UtcNow.AddMinutes(-5)));
        }
        else
        {
            query.Where(x => x.Status == status);
        }

        return await query
            .GroupBy(o => o.Status)
            .ToDictionaryAsync(x => x.Key, o => o.ToList(), cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken)
    {
        return await context
            .Order
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<int> UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        context.Order.Update(order);
        return await context.SaveChangesAsync(cancellationToken);
    }
}
