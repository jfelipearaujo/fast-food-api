using Application.UseCases.Orders.CheckoutHookOrder.Errors;
using Application.UseCases.Orders.Common.Errors;

using Domain.Adapters.Repositories;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.UseCases.Orders.CheckoutHookOrder;
using Domain.UseCases.Orders.CheckoutHookOrder.Requests;

using FluentResults;

namespace Application.UseCases.Orders.CheckoutHookOrder;

public class CheckoutHookOrderUseCase : ICheckoutHookOrderUseCase
{
    private readonly IOrderRepository orderRepository;
    private readonly IPaymentRepository paymentRepository;

    public CheckoutHookOrderUseCase(
        IOrderRepository orderRepository,
        IPaymentRepository paymentRepository)
    {
        this.orderRepository = orderRepository;
        this.paymentRepository = paymentRepository;
    }

    public async Task<Result> ExecuteAsync(
        CheckoutHookOrderRequest request,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(OrderId.Create(request.OrderId), cancellationToken);

        if (order is null)
        {
            return Result.Fail(new OrderNotFoundError(request.OrderId));
        }

        if (order.Status != OrderStatus.Created)
        {
            return Result.Fail(new OrderStatusInvalidToBePaidError());
        }

        var payment = order.GetWaitingApprovalPayment();

        if (payment is null)
        {
            return Result.Fail(new NoWaitingApprovalPaymentError());
        }

        if (request.PaymentApproved)
        {
            payment.UpdateToStatus(PaymentStatus.Approved);

            // Deliver the order to be prepared
            order.UpdateToStatus(OrderStatus.Received);

            await orderRepository.UpdateAsync(order, cancellationToken);
        }
        else
        {
            payment.UpdateToStatus(PaymentStatus.Rejected);
        }

        await paymentRepository.UpdateAsync(payment, cancellationToken);

        return Result.Ok();
    }
}
