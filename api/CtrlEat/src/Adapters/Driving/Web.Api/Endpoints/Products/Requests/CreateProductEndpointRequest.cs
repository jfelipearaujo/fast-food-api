using System.ComponentModel.DataAnnotations;

namespace Web.Api.Endpoints.Products.Requests
{
    public class CreateProductEndpointRequest
    {
        [Required]
        public Guid ProductCategoryId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
