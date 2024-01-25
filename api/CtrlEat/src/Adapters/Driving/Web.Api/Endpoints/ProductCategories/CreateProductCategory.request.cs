using System.ComponentModel.DataAnnotations;

namespace Web.Api.Endpoints.ProductCategories;

public class CreateProductCategoryEndpointRequest
{
    [Required]
    public string Description { get; set; }
}