using Domain.UseCases.ProductCategories.CreateProductCategory.Request;

namespace Utils.Tests.Builders.Application.ProductCategories.Requests;

public class CreateProductCategoryRequestBuilder
{
    private string description;

    public CreateProductCategoryRequestBuilder()
    {
        Reset();
    }

    public CreateProductCategoryRequestBuilder Reset()
    {
        description = default;

        return this;
    }

    public CreateProductCategoryRequestBuilder WithSample()
    {
        WithDescription("Product Category Description");

        return this;
    }

    public CreateProductCategoryRequestBuilder WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public CreateProductCategoryRequest Build()
    {
        return new CreateProductCategoryRequest
        {
            Description = description,
        };
    }
}
