using Application.UseCases.Clients.CreateClient;
using Application.UseCases.Clients.GetAllClients;
using Application.UseCases.Clients.GetClientById;
using Application.UseCases.Orders.AddOrderItem;
using Application.UseCases.Orders.CreateOrder;
using Application.UseCases.Orders.GetOrderById;
using Application.UseCases.ProductCategories.CreateProductCategory;
using Application.UseCases.ProductCategories.DeleteProductCategory;
using Application.UseCases.ProductCategories.GetAllProductCategories;
using Application.UseCases.ProductCategories.GetProductCategoryById;
using Application.UseCases.ProductCategories.UpdateProductCategory;
using Application.UseCases.Products.CreateProduct;
using Application.UseCases.Products.DeleteProduct;
using Application.UseCases.Products.GetAllProducts;
using Application.UseCases.Products.GetProductById;
using Application.UseCases.Products.GetProductsByCategory;
using Application.UseCases.Products.UpdateProduct;

using Domain.UseCases.Clients;
using Domain.UseCases.Orders;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.Products;

using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationDependency
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateProductCategoryUseCase, CreateProductCategoryUseCase>();
        services.AddScoped<IGetProductCategoryByIdUseCase, GetProductCategoryByIdUseCase>();
        services.AddScoped<IGetAllProductCategoriesUseCase, GetAllProductCategoriesUseCase>();
        services.AddScoped<IUpdateProductCategoryUseCase, UpdateProductCategoryUseCase>();
        services.AddScoped<IDeleteProductCategoryUseCase, DeleteProductCategoryUseCase>();

        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
        services.AddScoped<IGetProductsByCategoryUseCase, GetProductsByCategoryUseCase>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
        services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();

        services.AddScoped<ICreateClientUseCase, CreateClientUseCase>();
        services.AddScoped<IGetClientByIdUseCase, GetClientByIdUseCase>();
        services.AddScoped<IGetAllClientsUseCase, GetAllClientsUseCase>();

        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();
        services.AddScoped<IAddOrderItemUseCase, AddOrderItemUseCase>();

        return services;
    }
}