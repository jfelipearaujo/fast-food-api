using Domain.UseCases.Products.GetProductsByCategory;
using Domain.UseCases.Products.GetProductsByCategory.Requests;

using Web.Api.Endpoints.Products.Mapping;

namespace Web.Api.Endpoints.Products;

public static class GetAllProductsByCategory
{
    public static async Task<IResult> HandleAsync(
        string category,
        IGetProductsByCategoryUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new GetProductsByCategoryRequest
        {
            Category = category,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}