namespace Web.Api.Endpoints.Responses
{
    public class ProductCategoryResponse
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }
    }
}