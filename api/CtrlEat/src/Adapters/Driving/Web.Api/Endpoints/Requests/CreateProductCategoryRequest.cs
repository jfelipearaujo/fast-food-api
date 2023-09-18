using System.ComponentModel.DataAnnotations;

namespace Web.Api.Endpoints.Requests
{
    public class CreateProductCategoryRequest
    {
        [Required]
        public string Description { get; set; }
    }
}