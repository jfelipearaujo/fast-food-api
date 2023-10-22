using Domain.Entities.ClientAggregate.ValueObjects;
using Domain.UseCases.Orders.CreateOrder.Requests;

namespace Utils.Tests.Builders.Application.Orders.Requests;

public class CreateOrderRequestBuilder
{
    private Guid clientId;

    public CreateOrderRequestBuilder()
    {
        Reset();
    }

    public CreateOrderRequestBuilder Reset()
    {
        clientId = default;
        return this;
    }

    public CreateOrderRequestBuilder WithSample()
    {
        WithClientId(Guid.NewGuid());
        return this;
    }

    public CreateOrderRequestBuilder WithClientId(ClientId clientId)
    {
        this.clientId = clientId.Value;
        return this;
    }

    public CreateOrderRequestBuilder WithClientId(Guid clientId)
    {
        this.clientId = clientId;
        return this;
    }

    public CreateOrderRequest Build()
    {
        return new CreateOrderRequest
        {
            ClientId = clientId
        };
    }
}
