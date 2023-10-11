namespace Domain.UseCases.Products.CreateProduct.Requests;

public class CreateProductRequest
{
    public Guid ProductCategoryId { get; set; }

    public string Description { get; set; }

    public string Currency { get; set; }

    public decimal Amount { get; set; }

    public string ImageUrl { get; set; }
}
