using Domain.UseCases.ProductCategories.UpdateProductCategory;
using Domain.UseCases.ProductCategories.UpdateProductCategory.Request;

using Web.Api.Endpoints.ProductCategories.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.ProductCategories;

public static class UpdateProductCategory
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        UpdateProductCategoryEndpointRequest endpointRequest,
        IUpdateProductCategoryUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new UpdateProductCategoryRequest
        {
            Id = id,
            Description = endpointRequest.Description,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.CreatedAtRoute(
            ApiEndpoints.ProductCategories.V1.GetById,
            response,
            new
            {
                id = response.Id,
            });
    }
}