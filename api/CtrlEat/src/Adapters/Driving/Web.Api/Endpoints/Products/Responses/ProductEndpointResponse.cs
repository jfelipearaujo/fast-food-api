using Web.Api.Endpoints.ProductCategories.Responses;

namespace Web.Api.Endpoints.Products.Responses;

public class ProductEndpointResponse
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }

    public string ImageUrl { get; set; }

    public ProductCategoryEndpointResponse ProductCategory { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }
}
