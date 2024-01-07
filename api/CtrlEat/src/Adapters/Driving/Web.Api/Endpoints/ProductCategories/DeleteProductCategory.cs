using Domain.UseCases.ProductCategories.DeleteProductCategory;
using Domain.UseCases.ProductCategories.DeleteProductCategory.Request;

using Web.Api.Extensions;

namespace Web.Api.Endpoints.ProductCategories;

public static class DeleteProductCategory
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        IDeleteProductCategoryUseCase useCase,
        CancellationToken cancellationToken)
    {
        var useCaseRequest = new DeleteProductCategoryRequest
        {
            Id = id
        };

        var result = await useCase.ExecuteAsync(useCaseRequest, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        return Results.NoContent();
    }
}