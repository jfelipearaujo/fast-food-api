using Domain.UseCases.Products.DeleteProduct;
using Domain.UseCases.Products.DeleteProduct.Requests;

using Web.Api.Extensions;

namespace Web.Api.Endpoints.Products;

public static class DeleteProduct
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        IDeleteProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest
        {
            Id = id,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        return Results.NoContent();
    }
}