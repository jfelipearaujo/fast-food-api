using Application.UseCases.ProductCategories;

using Domain.UseCases.ProductCategories;

using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDependency
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ICreateProductCategoryUseCase, CreateProductCategoryUseCase>();
            services.AddTransient<IGetProductCategoryUseCase, GetProductCategoryUseCase>();
            services.AddTransient<IGetAllProductCategoryUseCase, GetAllProductCategoryUseCase>();
            services.AddTransient<IUpdateProductCategoryUseCase, UpdateProductCategoryUseCase>();
            services.AddTransient<IDeleteProductCategoryUseCase, DeleteProductCategoryUseCase>();

            return services;
        }
    }
}