using Domain.UseCases.Products.UpdateProduct;

using Web.Api.Endpoints.Products.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Products;

public static class UpdateProduct
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        UpdateProductEndpointRequest endpointRequest,
        IUpdateProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest(id);

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.CreatedAtRoute(
            ApiEndpoints.Products.V1.GetById,
            response,
            new
            {
                id = response.Id
            });
    }
}