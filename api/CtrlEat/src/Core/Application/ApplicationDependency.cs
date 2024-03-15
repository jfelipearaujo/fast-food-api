using Application.Services.StorageService;
using Application.UseCases.Clients.CreateClient;
using Application.UseCases.Clients.GetAllClients;
using Application.UseCases.Clients.GetClientByDocumentId;
using Application.UseCases.Clients.GetClientById;
using Application.UseCases.Orders.AddOrderItem;
using Application.UseCases.Orders.CheckoutHookOrder;
using Application.UseCases.Orders.CheckoutOrder;
using Application.UseCases.Orders.CreateOrder;
using Application.UseCases.Orders.GetOrderById;
using Application.UseCases.Orders.GetOrdersByStatus;
using Application.UseCases.Orders.UpdateOrderStatus;
using Application.UseCases.ProductCategories.CreateProductCategory;
using Application.UseCases.ProductCategories.DeleteProductCategory;
using Application.UseCases.ProductCategories.GetAllProductCategories;
using Application.UseCases.ProductCategories.GetProductCategoryById;
using Application.UseCases.ProductCategories.UpdateProductCategory;
using Application.UseCases.Products.CreateProduct;
using Application.UseCases.Products.DeleteProduct;
using Application.UseCases.Products.GetAllProducts;
using Application.UseCases.Products.GetProductById;
using Application.UseCases.Products.GetProductImage;
using Application.UseCases.Products.GetProductsByCategory;
using Application.UseCases.Products.UpdateProduct;
using Application.UseCases.Products.UploadProductImage;

using Domain.Adapters.Storage;
using Domain.UseCases.Clients.CreateClient;
using Domain.UseCases.Clients.GetAllClients;
using Domain.UseCases.Clients.GetClientByDocumentId;
using Domain.UseCases.Clients.GetClientById;
using Domain.UseCases.Orders.AddOrderItem;
using Domain.UseCases.Orders.CheckoutHookOrder;
using Domain.UseCases.Orders.CheckoutOrder;
using Domain.UseCases.Orders.CreateOrder;
using Domain.UseCases.Orders.GetOrderById;
using Domain.UseCases.Orders.GetOrdersByStatus;
using Domain.UseCases.Orders.UpdateOrderStatus;
using Domain.UseCases.ProductCategories.CreateProductCategory;
using Domain.UseCases.ProductCategories.DeleteProductCategory;
using Domain.UseCases.ProductCategories.GetAllProductCategories;
using Domain.UseCases.ProductCategories.GetProductCategoryById;
using Domain.UseCases.ProductCategories.UpdateProductCategory;
using Domain.UseCases.Products.CreateProduct;
using Domain.UseCases.Products.DeleteProduct;
using Domain.UseCases.Products.GetAllProducts;
using Domain.UseCases.Products.GetProductById;
using Domain.UseCases.Products.GetProductImage;
using Domain.UseCases.Products.GetProductsByCategory;
using Domain.UseCases.Products.UpdateProduct;
using Domain.UseCases.Products.UploadProductImage;

using Microsoft.Extensions.DependencyInjection;

using System.IO.Abstractions;

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
        services.AddScoped<IUploadProductImageUseCase, UploadProductImageUseCase>();
        services.AddScoped<IGetProductImageUseCase, GetProductImageUseCase>();

        services.AddScoped<ICreateClientUseCase, CreateClientUseCase>();
        services.AddScoped<IGetClientByIdUseCase, GetClientByIdUseCase>();
        services.AddScoped<IGetClientByDocumentIdUseCase, GetClientByDocumentIdUseCase>();
        services.AddScoped<IGetAllClientsUseCase, GetAllClientsUseCase>();

        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();
        services.AddScoped<IAddOrderItemUseCase, AddOrderItemUseCase>();
        services.AddScoped<IGetOrdersByStatusUseCase, GetOrdersByStatusUseCase>();
        services.AddScoped<IUpdateOrderStatusUseCase, UpdateOrderStatusUseCase>();
        services.AddScoped<ICheckoutOrderUseCase, CheckoutOrderUseCase>();
        services.AddScoped<ICheckoutHookOrderUseCase, CheckoutHookOrderUseCase>();

        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddScoped<IStorageService, StorageService>();

        return services;
    }
}