using Application.UseCases.Common.Errors;
using Application.UseCases.Orders.AddOrderItem;
using Application.UseCases.Orders.AddOrderItem.Errors;
using Application.UseCases.Orders.Common.Errors;

using Domain.Adapters.Repositories;
using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.Entities.ProductAggregate;
using Domain.Entities.ProductAggregate.ValueObjects;

using Utils.Tests.Builders.Application.Orders.Requests;
using Utils.Tests.Builders.Application.Orders.Responses;
using Utils.Tests.Builders.Domain.Entities;

namespace Application.Tests.UseCases.Orders;

public class AddOrderItemUseCaseTests
{
    private readonly IOrderRepository orderRepository;
    private readonly IProductRepository productRepository;

    private readonly AddOrderItemUseCase sut;

    public AddOrderItemUseCaseTests()
    {
        orderRepository = Substitute.For<IOrderRepository>();
        productRepository = Substitute.For<IProductRepository>();

        sut = new AddOrderItemUseCase(orderRepository, productRepository);
    }

    [Fact]
    public async Task ShouldAddOrderItemToAnOrderSuccessfully()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithSample()
            .Build();

        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategory(new ProductCategoryBuilder().WithSample().Build())
            .Build();

        orderRepository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        productRepository
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        var request = new AddOrderItemRequestBuilder()
            .WithSample()
            .WithOrderId(order.Id)
            .WithProductId(product.Id)
            .Build();

        var expected = new OrderItemResponseBuilder()
            .WithId(product.Id)
            .WithQuantity(request.Quantity)
            .WithObservation(request.Observation)
            .WithDescription(product.Description)
            .WithPrice(product.Price)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeSuccess();
        response.Should().Satisfy(result =>
        {
            result.Value.Should().BeEquivalentTo(expected, opt => opt.Excluding(x => x.Id));
        });

        await orderRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await productRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());

        await orderRepository
            .Received(1)
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenOrderIsNotFound()
    {
        // Arrange
        orderRepository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(default(Order));

        var request = new AddOrderItemRequestBuilder()
            .WithSample()
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new OrderNotFoundError(request.OrderId));

        await orderRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await productRepository
            .DidNotReceive()
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());

        await orderRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenProductIsNotFound()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithSample()
            .Build();

        orderRepository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        productRepository
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(default(Product));

        var request = new AddOrderItemRequestBuilder()
            .WithSample()
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new ProductNotFoundError(request.ProductId));

        await orderRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await productRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());

        await orderRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenProductAlreadyAddedOnOrder()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithSample()
            .Build();

        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategory(new ProductCategoryBuilder().WithSample().Build())
            .Build();

        order.AddItem(OrderItem.Create(1, string.Empty, product).Value);

        orderRepository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        productRepository
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        var request = new AddOrderItemRequestBuilder()
            .WithSample()
            .WithOrderId(order.Id)
            .WithProductId(product.Id)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();
        response.Should().HaveReason(new OrderItemAlreadyExistsError(product.Description));

        await orderRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await productRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());

        await orderRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ShouldReturnErrorWhenReceiveInvalidData()
    {
        // Arrange
        var order = new OrderBuilder()
            .WithSample()
            .Build();

        var product = new ProductBuilder()
            .WithSample()
            .WithProductCategory(new ProductCategoryBuilder().WithSample().Build())
            .Build();

        orderRepository
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>())
            .Returns(order);

        productRepository
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
            .Returns(product);

        var request = new AddOrderItemRequestBuilder()
            .WithSample()
            .WithOrderId(order.Id)
            .WithProductId(product.Id)
            .WithQuantity(0)
            .Build();

        // Act
        var response = await sut.ExecuteAsync(request, default);

        // Assert
        response.Should().BeFailure();

        await orderRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<OrderId>(), Arg.Any<CancellationToken>());

        await productRepository
            .Received(1)
            .GetByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>());

        await orderRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

}
