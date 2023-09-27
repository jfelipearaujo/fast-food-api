namespace Domain.UseCases.ProductCategories.Requests;

public class UpdateProductCategoryRequest
{
    public Guid Id { get; set; }

    public string Description { get; set; }
}