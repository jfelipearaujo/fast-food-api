using Domain.UseCases.Products.CreateProduct.Requests;
using Web.Api.Endpoints.Products.Requests;

namespace Web.Api.Endpoints.Products.Mapping;

public static class CreateProductEndpointRequestMapper
{
    public static CreateProductRequest MapToRequest(this CreateProductEndpointRequest request)
    {
        return new CreateProductRequest
        {
            ProductCategoryId = request.ProductCategoryId,
            Description = request.Description,
            Currency = request.Currency,
            Amount = request.Amount,
            ImageUrl = request.ImageUrl
        };
    }
}
