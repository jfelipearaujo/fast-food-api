using Domain.UseCases.ProductCategories.GetAllProductCategories;

using Web.Api.Endpoints.ProductCategories.Mapping;

namespace Web.Api.Endpoints.ProductCategories;

public static class GetAllProductCategories
{
    public static async Task<IResult> HandleAsync(
        IGetAllProductCategoriesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(cancellationToken);

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}