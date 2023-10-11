using Domain.Entities.OrderAggregate.Enums;

using System.Text.Json.Serialization;

namespace Web.Api.Endpoints.Orders.Responses;

public class OrderEndpointResponse
{
    public Guid Id { get; set; }

    public string TrackId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; }

    public List<OrderItemEndpointResponse> Items { get; set; }
}
