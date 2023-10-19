using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.ValueObjects;

namespace Domain.Adapters.Repositories;

public interface IPaymentRepository
{
    Task<int> CreateAsync(Payment payment, CancellationToken cancellationToken);

    Task<Payment?> GetByIdAsync(PaymentId id, CancellationToken cancellationToken);

    Task<IEnumerable<Payment>> GetAllAsync(CancellationToken cancellationToken);

    Task<int> UpdateAsync(Payment payment, CancellationToken cancellationToken);

    Task<int> DeleteAsync(Payment payment, CancellationToken cancellationToken);
}