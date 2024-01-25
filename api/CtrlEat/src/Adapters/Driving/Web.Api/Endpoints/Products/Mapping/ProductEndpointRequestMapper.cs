using Domain.UseCases.Products.CreateProduct.Requests;

namespace Web.Api.Endpoints.Products.Mapping;

public static class ProductEndpointRequestMapper
{
    public static CreateProductRequest MapToRequest(this CreateProductEndpointRequest request)
    {
        return new CreateProductRequest
        {
            ProductCategoryId = request.ProductCategoryId,
            Description = request.Description,
            Currency = request.Currency,
            Amount = request.Amount,
        };
    }
}
