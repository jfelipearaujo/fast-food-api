using Application.UseCases.Orders.CheckoutOrder;
using Application.UseCases.Orders.CheckoutOrder.Errors;
using Application.UseCases.Orders.Common.Errors;

using Domain.Adapters.Repositories;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;

using Utils.Tests.Builders.Application.Orders.Requests;
using Utils.Tests.Builders.Application.Orders.Responses;
using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Orders;

public class CheckoutOrderUseCaseTests
{
    private readonly IOrderRepository orderRepository;
    private readonly IPaymentRepository paymentRepository;

    private readonly CheckoutOrderUseCase sut;

    public CheckoutOrderUseCaseTests()
    {
        orderRepository = Substitute.For<IOrderRepository>();
        paymentRepository = Substitute.For<IPaymentRepository>();

        sut = new CheckoutOrderUseCase(orderRepository, paymentRepository);
    }

    [Fact]
    public async Task ShouldCheckoutAnOrderSuccessfully()
    {
        // Arrange
        var orderItem = new OrderItemBuilder()
            .WithSample()
            .Build();

        var order = new OrderBuilder()
            .WithSample()
            .WithStatus(OrderStatus.Created)
            .WithItem(orderItem)
            .Build();

        orderRepository.GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        var request = new CheckoutOrderRequestBuilder()
            .WithOrderId(order.Id)
            .Build();

        var expected = new CheckoutOrderResponseBuilder()
            .WithStatus(PaymentStatus.Approved)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeSuccess();
        response.Should().Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(expected);
        });

        await orderRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await paymentRepository
            .Received(1)
            .CreateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>());

        await paymentRepository
            .Received(1)
            .UpdateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenOrderIsNotFound()
    {
        // Arrange
        var orderItem = new OrderItemBuilder()
            .WithSample()
            .Build();

        var order = new OrderBuilder()
            .WithSample()
            .WithStatus(OrderStatus.Created)
            .WithItem(orderItem)
            .Build();

        orderRepository.GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(default(Order));

        var request = new CheckoutOrderRequestBuilder()
            .WithOrderId(order.Id)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new OrderNotFoundError(request.OrderId));

        await orderRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await paymentRepository
            .DidNotReceive()
            .CreateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>());

        await paymentRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenOrderStatusIsInvalid()
    {
        // Arrange
        var orderItem = new OrderItemBuilder()
            .WithSample()
            .Build();

        var order = new OrderBuilder()
            .WithSample()
            .WithStatus(OrderStatus.OnGoing)
            .WithItem(orderItem)
            .Build();

        orderRepository.GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        var request = new CheckoutOrderRequestBuilder()
            .WithOrderId(order.Id)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new OrderStatusInvalidToBePaidError(order.Status));

        await orderRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await paymentRepository
            .DidNotReceive()
            .CreateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>());

        await paymentRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenOrderAlreadyHasPaymentInProgress()
    {
        // Arrange
        var orderItem = new OrderItemBuilder()
            .WithSample()
            .Build();

        var order = new OrderBuilder()
            .WithSample()
            .WithStatus(OrderStatus.Created)
            .WithItem(orderItem)
            .Build();

        var payment = new PaymentBuilder()
            .WithOrder(order)
            .WithStatus(PaymentStatus.WaitingApproval)
            .Build();

        order.AddPayment(payment);

        orderRepository.GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        var request = new CheckoutOrderRequestBuilder()
            .WithOrderId(order.Id)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new PaymentAlreadyExistsForOrderError());

        await orderRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await paymentRepository
            .DidNotReceive()
            .CreateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>());

        await paymentRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenOrderDoesNotHaveItems()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithSample()
            .WithStatus(OrderStatus.Created)
            .Build();

        orderRepository.GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        var request = new CheckoutOrderRequestBuilder()
            .WithOrderId(order.Id)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new OrderWithoutMinimumItemsError(CheckoutOrderUseCase.MINIMUM_ORDER_ITEMS_TO_CHECKOUT));

        await orderRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await paymentRepository
            .DidNotReceive()
            .CreateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>());

        await paymentRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>());
    }
}
