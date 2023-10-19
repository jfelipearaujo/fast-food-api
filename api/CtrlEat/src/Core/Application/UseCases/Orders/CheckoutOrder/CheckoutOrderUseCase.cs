using Application.UseCases.Orders.CheckoutOrder.Errors;
using Application.UseCases.Orders.Common.Errors;

using Domain.Adapters.Repositories;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Orders.CheckoutOrder;
using Domain.UseCases.Orders.CheckoutOrder.Requests;
using Domain.UseCases.Orders.CheckoutOrder.Responses;

using FluentResults;

namespace Application.UseCases.Orders.CheckoutOrder;

public class CheckoutOrderUseCase : ICheckoutOrderUseCase
{
    private const int MINIMUM_ORDER_ITEMS_TO_CHECKOUT = 1;

    private readonly IOrderRepository orderRepository;
    private readonly IPaymentRepository paymentRepository;

    public CheckoutOrderUseCase(
        IOrderRepository orderRepository,
        IPaymentRepository paymentRepository)
    {
        this.orderRepository = orderRepository;
        this.paymentRepository = paymentRepository;
    }

    public async Task<Result<CheckoutOrderResponse>> ExecuteAsync(
        CheckoutOrderRequest request,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(
            OrderId.Create(request.OrderId),
            cancellationToken);

        if (order is null)
        {
            return Result.Fail(new OrderNotFoundError(request.OrderId));
        }

        if (order.Status != OrderStatus.Created)
        {
            return Result.Fail(new OrderStatusInvalidToBePaidError(order.Status));
        }

        if (order.Payments.Any(x => x.Status == PaymentStatus.WaitingApproval))
        {
            return Result.Fail(new PaymentAlreadyExistsForOrderError());
        }

        if (order.Items.Count < MINIMUM_ORDER_ITEMS_TO_CHECKOUT)
        {
            return Result.Fail(new OrderWithoutMinimumItemsError(MINIMUM_ORDER_ITEMS_TO_CHECKOUT));
        }

        var orderTotalAmount = order.Items.Sum(x => x.Quantity * x.Price.Amount);
        var orderCurrency = order.Items.Select(x => x.Price.Currency).First();

        var paymentPrice = Money.Create(orderTotalAmount, orderCurrency);

        if (paymentPrice.IsFailed)
        {
            return Result.Fail(paymentPrice.Errors);
        }

        var payment = Payment.Create(order.Id, paymentPrice.Value);

        await paymentRepository.CreateAsync(payment.Value, cancellationToken);

        // TODO: Fake approval
        payment.Value.UpdateToStatus(PaymentStatus.Approved);

        await paymentRepository.UpdateAsync(payment.Value, cancellationToken);

        return CheckoutOrderResponse.MapFromDomain(payment.Value);
    }
}
