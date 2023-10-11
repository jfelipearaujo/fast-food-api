using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.UpdateProduct.Requests;

using Web.Api.Endpoints.ProductCategories.Responses.Mapping;
using Web.Api.Endpoints.Products.Requests;
using Web.Api.Endpoints.Products.Responses.Mapping;

namespace Web.Api.Endpoints.Products.Responses.Mapping;

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
            response.Add(product.MapToResponse());
        }

        return response;
    }

    public static UpdateProductRequest MapToRequest(this UpdateProductEndpointRequest request, Guid productId)
    {
        return new UpdateProductRequest
        {
            ProductId = productId,
            ProductCategoryId = request.ProductCategoryId,
            Description = request.Description,
            Currency = request.Currency,
            Amount = request.Amount,
            ImageUrl = request.ImageUrl
        };
    }
}
