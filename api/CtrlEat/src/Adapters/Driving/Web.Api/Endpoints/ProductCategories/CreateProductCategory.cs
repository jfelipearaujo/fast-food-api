using Domain.UseCases.ProductCategories.CreateProductCategory;
using Domain.UseCases.ProductCategories.CreateProductCategory.Request;

using Web.Api.Endpoints.ProductCategories.Mapping;

namespace Web.Api.Endpoints.ProductCategories;

public static class CreateProductCategory
{
    public static async Task<IResult> HandleAsync(
        CreateProductCategoryEndpointRequest endpointRequest,
        ICreateProductCategoryUseCase useCase,
        HttpContext httpContext,
        LinkGenerator linkGenerator,
        CancellationToken cancellationToken)
    {
        var request = new CreateProductCategoryRequest
        {
            Description = endpointRequest.Description,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        var response = result.Value.MapToResponse();

        var location = linkGenerator.GetUriByName(
            httpContext,
            ApiEndpoints.ProductCategories.V1.GetById,
            new
            {
                id = response.Id
            });

        return Results.Created(
            location,
            response);
    }
}