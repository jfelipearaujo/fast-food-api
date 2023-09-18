using System.ComponentModel.DataAnnotations;

namespace Web.Api.Endpoints.Requests
{
    public class UpdateProductCategoryRequest
    {
        [Required]
        public string Description { get; set; }
    }
}