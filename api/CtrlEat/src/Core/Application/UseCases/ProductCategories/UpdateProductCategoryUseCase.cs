﻿using Domain.Adapters;
using Domain.Entities.StrongIds;
using Domain.Errors.ProductCategories;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using FluentResults;

namespace Application.UseCases.ProductCategories
{
    public class UpdateProductCategoryUseCase : IUpdateProductCategoryUseCase
    {
        private readonly IProductCategoryRepository repository;

        public UpdateProductCategoryUseCase(IProductCategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<ProductCategoryResponse>> ExecuteAsync(UpdateProductCategoryRequest request, CancellationToken cancellationToken)
        {
            var productCategory = await repository.GetByIdAsync(ProductCategoryId.Create(request.Id), cancellationToken);

            if (productCategory is null)
            {
                return Result.Fail(new ProductCategoryNotFoundError(request.Id));
            }

            productCategory.Description = request.Description;

            await repository.UpdateAsync(productCategory, cancellationToken);

            var response = ProductCategoryResponse.MapFromDomain(productCategory);

            return Result.Ok(response);
        }
    }
}