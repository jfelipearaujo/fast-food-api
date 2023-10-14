using Microsoft.AspNetCore.Http;

namespace Domain.UseCases.Products.UploadProductImage.Requests;

public class UploadProductImageRequest
{
    public Guid Id { get; set; }

    public IFormFile File { get; set; }

    public string ImageUrl { get; set; }
}