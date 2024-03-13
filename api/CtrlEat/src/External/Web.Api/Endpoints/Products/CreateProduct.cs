using Domain.UseCases.Products.CreateProduct;

using Web.Api.Endpoints.Products.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Products;

public static class CreateProduct
{
    public static async Task<IResult> HandleAsync(
        CreateProductEndpointRequest endpointRequest,
        ICreateProductUseCase useCase,
        HttpContext httpContext,
        LinkGenerator linkGenerator,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest();

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        var location = linkGenerator.GetUriByName(
            httpContext,
            ApiEndpoints.Products.V1.GetById,
            new
            {
                id = response.Id
            });

        return Results.Created(
            location,
            response);
    }
}