namespace Domain.UseCases.ProductCategories.Requests
{
    public class UpdateProductCategoryUseCaseRequest
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}