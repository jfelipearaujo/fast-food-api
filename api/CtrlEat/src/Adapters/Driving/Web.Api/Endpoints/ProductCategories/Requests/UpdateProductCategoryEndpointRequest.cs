using System.ComponentModel.DataAnnotations;

namespace Web.Api.Endpoints.ProductCategories.Requests
{
    public class UpdateProductCategoryEndpointRequest
    {
        [Required]
        public string Description { get; set; }
    }
}