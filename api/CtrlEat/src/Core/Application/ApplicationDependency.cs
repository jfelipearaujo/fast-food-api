﻿using Application.UseCases.Clients.CreateClient;
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
using Domain.UseCases.Clients.CreateClient;
using Domain.UseCases.Clients.GetAllClients;
using Domain.UseCases.Clients.GetClientById;
using Domain.UseCases.Orders.AddOrderItem;
using Domain.UseCases.Orders.CreateOrder;
using Domain.UseCases.Orders.GetOrderById;
using Domain.UseCases.ProductCategories.CreateProductCategory;
using Domain.UseCases.ProductCategories.DeleteProductCategory;
using Domain.UseCases.ProductCategories.GetAllProductCategories;
using Domain.UseCases.ProductCategories.GetProductCategoryById;
using Domain.UseCases.ProductCategories.UpdateProductCategory;
using Domain.UseCases.Products.CreateProduct;
using Domain.UseCases.Products.DeleteProduct;
using Domain.UseCases.Products.GetAllProducts;
using Domain.UseCases.Products.GetProductById;
using Domain.UseCases.Products.GetProductsByCategory;
using Domain.UseCases.Products.UpdateProduct;
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