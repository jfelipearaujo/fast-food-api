using Domain.UseCases.Products.UploadProductImage.Requests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Utils.Tests.Builders.Application.Products;

public class UploadProductImageRequestBuilder
{
    private Guid id;
    private string imageUrl;
    private IFormFile file;

    public UploadProductImageRequestBuilder()
    {
        Reset();
    }

    public UploadProductImageRequestBuilder Reset()
    {
        id = default;
        imageUrl = default;
        file = default;

        return this;
    }

    public UploadProductImageRequestBuilder WithSample()
    {
        WithId(Guid.NewGuid());
        WithImageUrl("https://www.google.com.br");
        WithFile(new FormFile(Stream.Null, 0, 0, "file", "image.jpg"));

        return this;
    }

    public UploadProductImageRequestBuilder WithId(Guid id)
    {
        this.id = id;
        return this;
    }

    public UploadProductImageRequestBuilder WithImageUrl(string imageUrl)
    {
        this.imageUrl = imageUrl;
        return this;
    }

    public UploadProductImageRequestBuilder WithFile(IFormFile file)
    {
        this.file = file;
        return this;
    }

    public UploadProductImageRequest Build()
    {
        return new()
        {
            Id = id,
            ImageUrl = imageUrl,
            File = file
        };
    }
}
