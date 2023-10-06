namespace Web.Api.Endpoints.ProductCategories.Responses;

public class ProductCategoryEndpointResponse
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }
}