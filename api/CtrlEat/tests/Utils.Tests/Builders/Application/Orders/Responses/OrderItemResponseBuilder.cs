using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Orders.Common.Responses;

using System.Globalization;

namespace Utils.Tests.Builders.Application.Orders.Responses;

public class OrderItemResponseBuilder
{
    private Guid id;
    private int quantity;
    private string observation;
    private string description;
    private string price;

    public OrderItemResponseBuilder()
    {
        Reset();
    }

    public OrderItemResponseBuilder Reset()
    {
        id = default;
        quantity = default;
        observation = default;
        description = default;
        price = default;

        return this;
    }

    public OrderItemResponseBuilder WithSample()
    {
        WithId(Guid.NewGuid());
        WithQuantity(1);
        WithObservation("Observation");
        WithDescription("Description");
        WithPrice("R$ 10,90");

        return this;
    }

    public OrderItemResponseBuilder WithId(ProductId id)
    {
        this.id = id.Value;
        return this;
    }

    public OrderItemResponseBuilder WithId(Guid id)
    {
        this.id = id;
        return this;
    }

    public OrderItemResponseBuilder WithQuantity(int quantity)
    {
        this.quantity = quantity;
        return this;
    }

    public OrderItemResponseBuilder WithObservation(string observation)
    {
        this.observation = observation;
        return this;
    }

    public OrderItemResponseBuilder WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public OrderItemResponseBuilder WithPrice(Money price)
    {
        var currencyCultureName = price.Currency == Money.BRL ? "pt-BR" : "en-US";

        this.price = (quantity * price.Amount).ToString("C", CultureInfo.CreateSpecificCulture(currencyCultureName));
        return this;
    }

    public OrderItemResponseBuilder WithPrice(string price)
    {
        this.price = price;
        return this;
    }

    public OrderItemResponse Build()
    {
        return new OrderItemResponse
        {
            Id = id,
            Quantity = quantity,
            Observation = observation,
            Description = description,
            Price = price,
        };
    }
}
