using Domain.UseCases.Products.GetAllProducts;

using Web.Api.Endpoints.Products.Mapping;

namespace Web.Api.Endpoints.Products;

public static class GetAllProducts
{
    public static async Task<IResult> HandleAsync(
        IGetAllProductsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(cancellationToken);

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}