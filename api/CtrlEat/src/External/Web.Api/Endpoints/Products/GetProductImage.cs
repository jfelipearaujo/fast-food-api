using Domain.UseCases.Products.GetProductImage;
using Domain.UseCases.Products.GetProductImage.Requests;

using Web.Api.Common.Constants;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Products;

public static class GetProductImage
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        IGetProductImageUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new GetProductImageRequest
        {
            Id = id,
        };

        var result = await useCase.Execute(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        return Results.Stream(result.Value, ContentTypes.ApplicationOctetStream, $"image-{request.Id}.jpg");
    }
}