using Domain.UseCases.Products.UploadProductImage;
using Domain.UseCases.Products.UploadProductImage.Requests;

using Web.Api.Extensions;

namespace Web.Api.Endpoints.Products;

public static class UploadProductImage
{
    public static async Task<IResult> HandleAsync(
        Guid id,
        IFormFile file,
        IUploadProductImageUseCase useCase,
        HttpContext httpContext,
        LinkGenerator linkGenerator,
        CancellationToken cancellationToken)
    {
        var fileUrl = linkGenerator.GetUriByRouteValues(
            httpContext,
            nameof(GetProductImage),
            new
            {
                id
            });

        var request = new UploadProductImageRequest
        {
            Id = id,
            File = file,
            ImageUrl = fileUrl,
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        return Results.CreatedAtRoute(
            ApiEndpoints.Products.V1.GetImage,
            new
            {
                id = result.Value
            });
    }
}