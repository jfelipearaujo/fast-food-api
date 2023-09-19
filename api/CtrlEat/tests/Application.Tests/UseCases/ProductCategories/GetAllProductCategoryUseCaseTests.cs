﻿using Application.UseCases.ProductCategories;

using Domain.Adapters;
using Domain.Entities;
using Domain.UseCases.ProductCategories.Responses;

namespace Application.Tests.UseCases.ProductCategories
{
    public class GetAllProductCategoryUseCaseTests
    {
        private readonly GetAllProductCategoriesUseCase sut;

        private readonly IProductCategoryRepository productCategoryRepository;

        public GetAllProductCategoryUseCaseTests()
        {
            productCategoryRepository = Substitute.For<IProductCategoryRepository>();

            sut = new GetAllProductCategoriesUseCase(productCategoryRepository);
        }

        [Fact]
        public async Task ShouldGetAllProductCategorySuccessfully()
        {
            // Arrange
            var productCategoryOne = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = "Product Category 1"
            };

            var productCategoryTwo = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Description = "Product Category 2"
            };

            productCategoryRepository
                .GetAllAsync(Arg.Any<CancellationToken>())
                .Returns(new List<ProductCategory>
                {
                    productCategoryOne,
                    productCategoryTwo
                });

            // Act
            var result = await sut.ExecuteAsync(cancellationToken: default);

            // Assert
            result.Should().NotBeNullOrEmpty();

            result.Should().BeEquivalentTo(new List<ProductCategoryResponse>
            {
                new ProductCategoryResponse
                {
                    Id = productCategoryOne.Id,
                    Description = productCategoryOne.Description,
                },
                new ProductCategoryResponse
                {
                    Id = productCategoryTwo.Id,
                    Description = productCategoryTwo.Description,
                }
            });
        }

        [Fact]
        public async Task ShouldReturnNothingIfNothingWasFound()
        {
            // Arrange
            productCategoryRepository
                .GetAllAsync(Arg.Any<CancellationToken>())
                .Returns(new List<ProductCategory>());

            // Act
            var result = await sut.ExecuteAsync(cancellationToken: default);

            // Assert
            result.Should().BeNullOrEmpty();
        }
    }
}
