using Domain.UseCases.Products.Common.Responses;
using Web.Api.Endpoints.ProductCategories.Mapping;
using Web.Api.Endpoints.Products.Responses;

namespace Web.Api.Endpoints.Products.Mapping;

public static class ProductEndpointResponseMapper
{
    public static ProductEndpointResponse MapToResponse(this ProductResponse product)
    {
        return new ProductEndpointResponse
        {
            Id = product.Id,
            Description = product.Description,
            Currency = product.Currency,
            Amount = product.Amount,
            ImageUrl = product.ImageUrl,
            ProductCategory = product.ProductCategory.MapToResponse(),
            CreatedAtUtc = product.CreatedAtUtc,
            UpdatedAtUtc = product.UpdatedAtUtc,
        };
    }

    public static List<ProductEndpointResponse> MapToResponse(this List<ProductResponse> products)
    {
        var response = new List<ProductEndpointResponse>();

        foreach (var product in products)
        {
            response.Add(MapToResponse(product));
        }

        return response;
    }
}
