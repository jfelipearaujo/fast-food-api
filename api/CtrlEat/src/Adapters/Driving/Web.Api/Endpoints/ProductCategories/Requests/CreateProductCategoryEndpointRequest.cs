using System.ComponentModel.DataAnnotations;

namespace Web.Api.Endpoints.ProductCategories.Requests;

public class CreateProductCategoryEndpointRequest
{
    [Required]
    public string Description { get; set; }
}