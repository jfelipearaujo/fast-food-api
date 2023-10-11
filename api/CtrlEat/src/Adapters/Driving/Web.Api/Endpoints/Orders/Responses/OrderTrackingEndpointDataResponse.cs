namespace Web.Api.Endpoints.Orders.Responses;

public class OrderTrackingEndpointDataResponse
{
    public Guid Id { get; set; }

    public string TrackId { get; set; }

    public DateTime StatusUpdatedAt { get; set; }

    public string StatusUpdatedAtFormatted { get; set; }
}
