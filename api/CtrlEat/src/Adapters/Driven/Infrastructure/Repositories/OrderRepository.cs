using Domain.Adapters;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;

using Infrastructure.Common.Extensions;

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

    public async Task<IEnumerable<Order>> GetAllByStatusAsync(OrderStatus status, CancellationToken cancellationToken)
    {
        var filterTime = DateTime.UtcNow.AddMinutes(-5);

        return await context.Order
            .WhereIfElse(
                status != OrderStatus.None,
                x => x.Status == status,
                x => x.Status != OrderStatus.Completed || (x.Status == OrderStatus.Completed && x.StatusUpdatedAt >= filterTime))
            .ToListAsync(cancellationToken);
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
