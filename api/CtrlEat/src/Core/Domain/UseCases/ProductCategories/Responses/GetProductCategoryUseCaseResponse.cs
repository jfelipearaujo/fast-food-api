﻿namespace Domain.UseCases.ProductCategories.Responses
{
    public class GetProductCategoryUseCaseResponse
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }
    }
}