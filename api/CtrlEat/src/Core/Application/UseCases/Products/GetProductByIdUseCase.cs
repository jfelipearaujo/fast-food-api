﻿using Domain.Adapters;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using Mapster;

namespace Application.UseCases.Products
{
    public class GetProductByIdUseCase : IGetProductByIdUseCase
    {
        private readonly IProductRepository repository;

        public GetProductByIdUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProductResponse?> ExecuteAsync(
            GetProductByIdRequest request,
            CancellationToken cancellationToken)
        {
            var product = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                return null;

            return product.Adapt<ProductResponse?>();
        }
    }
}
