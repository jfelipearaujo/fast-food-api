using Domain.UseCases.ProductCategories.GetProductCategoryById;
using Domain.UseCases.ProductCategories.GetProductCategoryById.Request;

using Web.Api.Endpoints.ProductCategories.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.ProductCategories;

public static class GetProductCategoryById
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        IGetProductCategoryByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var useCaseRequest = new GetProductCategoryByIdRequest
        {
            Id = id
        };

        var result = await useCase.ExecuteAsync(useCaseRequest, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}