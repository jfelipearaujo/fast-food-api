using Domain.Adapters.Repositories;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext context;

    public PaymentRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<int> CreateAsync(Payment payment, CancellationToken cancellationToken)
    {
        context.Payment.Add(payment);

        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Payment payment, CancellationToken cancellationToken)
    {
        context.Payment.Remove(payment);

        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Payment.ToListAsync(cancellationToken);
    }

    public async Task<Payment?> GetByIdAsync(PaymentId id, CancellationToken cancellationToken)
    {
        return await context.Payment.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<int> UpdateAsync(Payment payment, CancellationToken cancellationToken)
    {
        context.Payment.Update(payment);

        return await context.SaveChangesAsync(cancellationToken);
    }
}
