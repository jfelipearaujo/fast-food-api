using Domain.UseCases.Products.Requests;

using Web.Api.Endpoints.Products.Requests;

namespace Web.Api.Endpoints.Products.Mapping
{
    public static class UpdateProductEndpointRequestMapper
    {
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
}
