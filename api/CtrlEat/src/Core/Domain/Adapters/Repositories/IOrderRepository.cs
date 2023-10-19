using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;

namespace Domain.Adapters.Repositories;

public interface IOrderRepository
{
    Task<int> CreateAsync(Order order, CancellationToken cancellationToken);

    Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken);

    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken);

    Task<IEnumerable<Order>> GetAllByStatusAsync(OrderStatus status, CancellationToken cancellationToken);

    Task<int> UpdateAsync(Order order, CancellationToken cancellationToken);

    Task<int> DeleteAsync(Order order, CancellationToken cancellationToken);
}
