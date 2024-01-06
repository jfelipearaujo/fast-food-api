using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;
using Domain.UseCases.Orders.CheckoutOrder.Responses;

using Utils.Tests.Builders.Domain.ValueObjects;

namespace Utils.Tests.Builders.Application.Orders.Responses;

public class CheckoutOrderResponseBuilder
{
    private TrackId trackId;
    private PaymentStatus status;

    public CheckoutOrderResponseBuilder()
    {
        Reset();
    }

    public CheckoutOrderResponseBuilder Reset()
    {
        trackId = default;
        status = default;

        return this;
    }

    public CheckoutOrderResponseBuilder WithSample()
    {
        trackId = new TrackIdBuilder().WithSample().Build();
        status = PaymentStatus.WaitingApproval;
        return this;
    }

    public CheckoutOrderResponseBuilder WithTrackId(TrackId trackId)
    {
        this.trackId = trackId;
        return this;
    }

    public CheckoutOrderResponseBuilder WithStatus(PaymentStatus status)
    {
        this.status = status;
        return this;
    }

    public CheckoutOrderResponse Build()
    {
        return new CheckoutOrderResponse
        {
            TrackId = trackId.Value,
            PaymentStatus = status
        };
    }
}
