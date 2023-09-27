﻿using FluentResults;

namespace Domain.Entities.ProductCategoryAggregate.Errors;

public class ProductCategoryDescriptionMaxLengthError : Error
{
    public ProductCategoryDescriptionMaxLengthError(int maxLength)
        : base($"A descrição da categoria de produtos deve ter no máximo ${maxLength} caracteres")
    {
    }
}
