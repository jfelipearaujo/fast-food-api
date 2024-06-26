﻿using System.ComponentModel.DataAnnotations;

namespace Web.Api.Endpoints.Products;

public class CreateProductEndpointRequest
{
    [Required]
    public Guid ProductCategoryId { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string Currency { get; set; }
}
