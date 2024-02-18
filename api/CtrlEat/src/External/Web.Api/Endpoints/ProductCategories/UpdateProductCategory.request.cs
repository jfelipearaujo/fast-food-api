using System.ComponentModel.DataAnnotations;

namespace Web.Api.Endpoints.ProductCategories;

public class UpdateProductCategoryEndpointRequest
{
    [Required]
    public string Description { get; set; }
}