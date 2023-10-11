using Domain.Entities.OrderAggregate;

using Humanizer;

using System.Globalization;

namespace Domain.UseCases.Orders.Common.Responses;

public class OrderTrackingDataResponse
{
    public Guid Id { get; set; }

    public string TrackId { get; set; }

    public DateTime StatusUpdatedAt { get; set; }

    public string StatusUpdatedAtFormatted { get; set; }

    // --

    public static OrderTrackingDataResponse MapFromDomain(Order order)
    {
        var statusSince = DateTime.UtcNow.Subtract(order.StatusUpdatedAt);

        return new OrderTrackingDataResponse
        {
            Id = order.Id.Value,
            TrackId = order.TrackId.Value,
            StatusUpdatedAt = order.StatusUpdatedAt,
            StatusUpdatedAtFormatted = statusSince.Humanize(culture: new CultureInfo("pt-BR"))
        };
    }
}
