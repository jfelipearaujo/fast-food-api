using Domain.UseCases.ProductCategories.Common.Responses;
using Web.Api.Endpoints.ProductCategories.Responses;

namespace Web.Api.Endpoints.ProductCategories.Mapping;

public static class ProductCategoryEndpointResponseMapper
{
    public static ProductCategoryEndpointResponse MapToResponse(this ProductCategoryResponse productCategory)
    {
        return new ProductCategoryEndpointResponse
        {
            Id = productCategory.Id,
            Description = productCategory.Description,
            CreatedAtUtc = productCategory.CreatedAtUtc,
            UpdatedAtUtc = productCategory.UpdatedAtUtc,
        };
    }

    public static List<ProductCategoryEndpointResponse> MapToResponse(this List<ProductCategoryResponse> productCategories)
    {
        var response = new List<ProductCategoryEndpointResponse>();

        foreach (var productCategory in productCategories)
        {
            response.Add(MapToResponse(productCategory));
        }

        return response;
    }
}
