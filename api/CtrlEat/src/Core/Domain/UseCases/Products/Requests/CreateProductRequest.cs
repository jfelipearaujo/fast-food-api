﻿namespace Domain.UseCases.Products.Requests
{
    public class CreateProductRequest
    {
        public Guid ProductCategoryId { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string Currency { get; set; }

        public string ImageUrl { get; set; }
    }
}