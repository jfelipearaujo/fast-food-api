namespace Domain.UseCases.ProductCategories.UpdateProductCategory.Request;

public class UpdateProductCategoryRequest
{
    public Guid Id { get; set; }

    public string Description { get; set; }
}