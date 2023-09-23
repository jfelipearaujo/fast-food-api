namespace Domain.UseCases.Products.Requests
{
    public class UpdateProductRequest
    {
        public Guid ProductId { get; set; }

        public Guid ProductCategoryId { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string ImageUrl { get; set; }
    }
}
