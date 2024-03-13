using Domain.UseCases.Products.GetProductById;
using Domain.UseCases.Products.GetProductById.Requests;

using Web.Api.Endpoints.Products.Mapping;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Products;

public static class GetProductById
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        IGetProductByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new GetProductByIdRequest
        {
            Id = id
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}