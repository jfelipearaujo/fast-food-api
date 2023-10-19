using Application.UseCases.Common.Errors;
using Application.UseCases.Orders.AddOrderItem.Errors;
using Application.UseCases.Orders.Common.Errors;
using Domain.Adapters.Repositories;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Orders.AddOrderItem;
using Domain.UseCases.Orders.AddOrderItem.Requests;
using Domain.UseCases.Orders.Common.Responses;

using FluentResults;

namespace Application.UseCases.Orders.AddOrderItem;

public class AddOrderItemUseCase : IAddOrderItemUseCase
{
    private readonly IOrderRepository orderRepository;
    private readonly IProductRepository productRepository;

    public AddOrderItemUseCase(IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        this.orderRepository = orderRepository;
        this.productRepository = productRepository;
    }

    public async Task<Result<OrderItemResponse>> ExecuteAsync(
        AddOrderItemRequest request,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(
            OrderId.Create(request.OrderId),
            cancellationToken);

        if (order is null)
        {
            return Result.Fail(new OrderNotFoundError(request.OrderId));
        }

        var product = await productRepository.GetByIdAsync(
            ProductId.Create(request.ProductId),
            cancellationToken);

        if (product is null)
        {
            return Result.Fail(new ProductNotFoundError(request.ProductId));
        }

        if (order.Items is not null
            && order.Items.Any(x => x.ProductId == product.Id))
        {
            return Result.Fail(new OrderItemAlreadyExistsError(product.Description));
        }

        var orderItemValueObject = OrderItem.Create(
            request.Quantity,
            request.Observation,
            product);

        if (orderItemValueObject.IsFailed)
        {
            return Result.Fail(orderItemValueObject.Errors);
        }

        OrderItem orderItem = orderItemValueObject.Value;

        order.AddItem(orderItem);

        await orderRepository.UpdateAsync(order, cancellationToken);

        return OrderItemResponse.MapFromDomain(orderItem);
    }
}
