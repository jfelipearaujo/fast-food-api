# Summary
<details open><summary>Summary</summary>

|||
|:---|:---|
| Generated on: | 10/24/2023 - 15:09:13 |
| Coverage date: | 10/23/2023 - 20:12:18 - 10/24/2023 - 14:46:41 |
| Parser: | MultiReport (14x Cobertura) |
| Assemblies: | 6 |
| Classes: | 187 |
| Files: | 182 |
| **Line coverage:** | 77.7% (3617 of 4651) |
| Covered lines: | 3617 |
| Uncovered lines: | 1034 |
| Coverable lines: | 4651 |
| Total lines: | 8389 |
| **Branch coverage:** | 73.3% (330 of 450) |
| Covered branches: | 330 |
| Total branches: | 450 |
| **Method coverage:** | [Feature is only available for sponsors](https://reportgenerator.io/pro) |

</details>

## Coverage
<details><summary>Application - 98.8%</summary>

|**Name**|**Line**|**Branch**|
|:---|---:|---:|
|**Application**|**98.8%**|**96.9%**|
|Application.ApplicationDependency|100%||
|Application.UseCases.Clients.CreateClient.CreateClientUseCase|100%|100%|
|Application.UseCases.Clients.CreateClient.Errors.ClientRegistrationDocument<br/>IdAlreadyExistsError|100%||
|Application.UseCases.Clients.CreateClient.Errors.ClientRegistrationEmailAlr<br/>eadyExistsError|100%||
|Application.UseCases.Clients.CreateClient.Errors.ClientRegistrationWithoutE<br/>mailError|100%||
|Application.UseCases.Clients.GetAllClients.GetAllClientsUseCase|100%|100%|
|Application.UseCases.Clients.GetClientById.GetClientByIdUseCase|100%|100%|
|Application.UseCases.Common.Constants.Images|100%||
|Application.UseCases.Common.Errors.ClientNotFoundError|100%||
|Application.UseCases.Common.Errors.ProductCategoryNotFoundError|100%||
|Application.UseCases.Common.Errors.ProductNotFoundError|100%||
|Application.UseCases.Orders.AddOrderItem.AddOrderItemUseCase|100%|90%|
|Application.UseCases.Orders.AddOrderItem.Errors.OrderItemAlreadyExistsError|100%||
|Application.UseCases.Orders.CheckoutOrder.CheckoutOrderUseCase|100%|100%|
|Application.UseCases.Orders.CheckoutOrder.Errors.OrderStatusInvalidToBePaid<br/>Error|100%||
|Application.UseCases.Orders.CheckoutOrder.Errors.OrderWithoutMinimumItemsEr<br/>ror|100%||
|Application.UseCases.Orders.CheckoutOrder.Errors.PaymentAlreadyExistsForOrd<br/>erError|100%||
|Application.UseCases.Orders.Common.Errors.InvalidOrderStatusError|100%||
|Application.UseCases.Orders.Common.Errors.OrderNotFoundError|100%||
|Application.UseCases.Orders.CreateOrder.CreateOrderUseCase|90.9%|75%|
|Application.UseCases.Orders.CreateOrder.Errors.OrderOnGoingForClientError|0%||
|Application.UseCases.Orders.GetOrderById.GetOrderByIdUseCase|100%|100%|
|Application.UseCases.Orders.GetOrdersByStatus.GetOrdersByStatusUseCase|100%|100%|
|Application.UseCases.Orders.UpdateOrderStatus.UpdateOrderStatusUseCase|100%|87.5%|
|Application.UseCases.ProductCategories.CreateProductCategory.CreateProductC<br/>ategoryUseCase|100%|100%|
|Application.UseCases.ProductCategories.DeleteProductCategory.DeleteProductC<br/>ategoryUseCase|100%|100%|
|Application.UseCases.ProductCategories.GetAllProductCategories.GetAllProduc<br/>tCategoriesUseCase|100%|100%|
|Application.UseCases.ProductCategories.GetProductCategoryById.GetProductCat<br/>egoryByIdUseCase|100%|100%|
|Application.UseCases.ProductCategories.UpdateProductCategory.UpdateProductC<br/>ategoryUseCase|100%|100%|
|Application.UseCases.Products.CreateProduct.CreateProductUseCase|100%|100%|
|Application.UseCases.Products.DeleteProduct.DeleteProductUseCase|100%|100%|
|Application.UseCases.Products.GetAllProducts.GetAllProductsUseCase|100%|100%|
|Application.UseCases.Products.GetProductById.GetProductByIdUseCase|100%|100%|
|Application.UseCases.Products.GetProductImage.Errors.ProductImageNotFountEr<br/>ror|100%||
|Application.UseCases.Products.GetProductImage.GetProductImageUseCase|100%|100%|
|Application.UseCases.Products.GetProductsByCategory.GetProductsByCategoryUs<br/>eCase|100%|100%|
|Application.UseCases.Products.UpdateProduct.UpdateProductUseCase|100%|100%|
|Application.UseCases.Products.UploadProductImage.Errors.ProductImageInvalid<br/>ExtensionError|100%||
|Application.UseCases.Products.UploadProductImage.UploadProductImageUseCase|100%|100%|

</details>
<details><summary>Domain - 91.8%</summary>

|**Name**|**Line**|**Branch**|
|:---|---:|---:|
|**Domain**|**91.8%**|**85.5%**|
|Domain.Entities.ClientAggregate.Client|100%|100%|
|Domain.Entities.ClientAggregate.Errors.EmailInvalidAddressError|100%||
|Domain.Entities.ClientAggregate.Errors.FullNameInvalidLengthError|100%||
|Domain.Entities.ClientAggregate.Errors.FullNameMissingFirstNameError|100%||
|Domain.Entities.ClientAggregate.Errors.InvalidDocumentIdError|100%||
|Domain.Entities.ClientAggregate.Validators.CpfValidator|100%|100%|
|Domain.Entities.ClientAggregate.ValueObjects.ClientId|100%||
|Domain.Entities.ClientAggregate.ValueObjects.DocumentId|100%|100%|
|Domain.Entities.ClientAggregate.ValueObjects.DocumentIdExtensions|100%|50%|
|Domain.Entities.ClientAggregate.ValueObjects.Email|100%|100%|
|Domain.Entities.ClientAggregate.ValueObjects.EmailExtensions|100%|50%|
|Domain.Entities.ClientAggregate.ValueObjects.FullName|100%|100%|
|Domain.Entities.ClientAggregate.ValueObjects.FullNameExtensions|100%|50%|
|Domain.Entities.OrderAggregate.Errors.OrderAlreadyWithStatusError|100%||
|Domain.Entities.OrderAggregate.Errors.OrderInvalidStatusTransitionError|100%||
|Domain.Entities.OrderAggregate.Errors.OrderItemInvalidQuantityError|100%||
|Domain.Entities.OrderAggregate.Errors.PaymentAlreadyWithStatusError|100%||
|Domain.Entities.OrderAggregate.Errors.PaymentInvalidStatusTransitionError|100%||
|Domain.Entities.OrderAggregate.Order|100%|100%|
|Domain.Entities.OrderAggregate.OrderItem|94.4%|83.3%|
|Domain.Entities.OrderAggregate.Payment|100%|100%|
|Domain.Entities.OrderAggregate.ValueObjects.OrderId|100%||
|Domain.Entities.OrderAggregate.ValueObjects.OrderItemId|57.1%||
|Domain.Entities.OrderAggregate.ValueObjects.PaymentId|100%||
|Domain.Entities.OrderAggregate.ValueObjects.TrackId|100%||
|Domain.Entities.ProductAggregate.Errors.MoneyInvalidAmountError|100%||
|Domain.Entities.ProductAggregate.Errors.MoneyInvalidCurrencyError|100%||
|Domain.Entities.ProductAggregate.Errors.ProductCategoryDescriptionInvalidEr<br/>ror|100%||
|Domain.Entities.ProductAggregate.Errors.ProductCategoryDescriptionMaxLength<br/>Error|100%||
|Domain.Entities.ProductAggregate.Errors.StockInvalidQuantityError|100%||
|Domain.Entities.ProductAggregate.Product|100%|87.5%|
|Domain.Entities.ProductAggregate.ProductCategory|100%|100%|
|Domain.Entities.ProductAggregate.Stock|92.8%|100%|
|Domain.Entities.ProductAggregate.ValueObjects.Money|100%|100%|
|Domain.Entities.ProductAggregate.ValueObjects.ProductCategoryId|100%||
|Domain.Entities.ProductAggregate.ValueObjects.ProductId|100%||
|Domain.Entities.ProductAggregate.ValueObjects.StockId|57.1%||
|Domain.UseCases.Clients.Common.Responses.ClientResponse|100%|100%|
|Domain.UseCases.Clients.CreateClient.Requests.CreateClientRequest|100%||
|Domain.UseCases.Clients.GetClientById.Requests.GetClientByIdRequest|100%||
|Domain.UseCases.Orders.AddOrderItem.Requests.AddOrderItemRequest|100%||
|Domain.UseCases.Orders.CheckoutOrder.Requests.CheckoutOrderRequest|100%||
|Domain.UseCases.Orders.CheckoutOrder.Responses.CheckoutOrderResponse|100%||
|Domain.UseCases.Orders.Common.Responses.OrderItemResponse|80.7%|50%|
|Domain.UseCases.Orders.Common.Responses.OrderResponse|65.2%|0%|
|Domain.UseCases.Orders.Common.Responses.OrderTrackingDataResponse|100%||
|Domain.UseCases.Orders.Common.Responses.OrderTrackingResponse|100%||
|Domain.UseCases.Orders.Common.Responses.PaymentResponse|0%|0%|
|Domain.UseCases.Orders.CreateOrder.Requests.CreateOrderRequest|100%||
|Domain.UseCases.Orders.GetOrderById.Requests.GetOrderByIdRequest|100%||
|Domain.UseCases.Orders.GetOrdersByStatus.Requests.GetOrdersByStatusRequest|100%||
|Domain.UseCases.Orders.UpdateOrderStatus.Requests.UpdateOrderStatusRequest|100%||
|Domain.UseCases.ProductCategories.Common.Responses.ProductCategoryResponse|100%||
|Domain.UseCases.ProductCategories.CreateProductCategory.Request.CreateProdu<br/>ctCategoryRequest|100%||
|Domain.UseCases.ProductCategories.DeleteProductCategory.Request.DeleteProdu<br/>ctCategoryRequest|100%||
|Domain.UseCases.ProductCategories.GetProductCategoryById.Request.GetProduct<br/>CategoryByIdRequest|100%||
|Domain.UseCases.ProductCategories.UpdateProductCategory.Request.UpdateProdu<br/>ctCategoryRequest|100%||
|Domain.UseCases.Products.Common.Responses.ProductResponse|100%||
|Domain.UseCases.Products.CreateProduct.Requests.CreateProductRequest|100%||
|Domain.UseCases.Products.DeleteProduct.Requests.DeleteProductRequest|100%||
|Domain.UseCases.Products.GetProductById.Requests.GetProductByIdRequest|100%||
|Domain.UseCases.Products.GetProductImage.Requests.GetProductImageRequest|100%||
|Domain.UseCases.Products.GetProductsByCategory.Requests.GetProductsByCatego<br/>ryRequest|100%||
|Domain.UseCases.Products.UpdateProduct.Requests.UpdateProductRequest|100%||
|Domain.UseCases.Products.UploadProductImage.Requests.UploadProductImageRequ<br/>est|100%||
|System.Text.RegularExpressions.Generated|84.4%|73.2%|

</details>
<details><summary>Infrastructure - 96.1%</summary>

|**Name**|**Line**|**Branch**|
|:---|---:|---:|
|**Infrastructure**|**96.1%**|**100%**|
|Infrastructure.Common.Extensions.QueryableExtensions|100%|100%|
|Infrastructure.InfrastructureDependency|100%||
|Infrastructure.Repositories.ClientRepository|100%||
|Infrastructure.Repositories.OrderRepository|85.3%||
|Infrastructure.Repositories.PaymentRepository|100%||
|Infrastructure.Repositories.ProductCategoryRepository|100%||
|Infrastructure.Repositories.ProductRepository|100%||

</details>
<details><summary>Persistence - 65.6%</summary>

|**Name**|**Line**|**Branch**|
|:---|---:|---:|
|**Persistence**|**65.6%**|**100%**|
|Persistence.AppDbContext|90%|100%|
|Persistence.Configurations.ClientEntityTypeConfiguration|100%||
|Persistence.Configurations.OrderEntityTypeConfiguration|100%||
|Persistence.Configurations.OrderItemEntityTypeConfiguration|100%||
|Persistence.Configurations.PaymentEntityTypeConfiguration|100%||
|Persistence.Configurations.ProductCategoryEntityTypeConfiguration|100%||
|Persistence.Configurations.ProductEntityTypeConfiguration|100%||
|Persistence.Configurations.StockEntityTypeConfiguration|100%||
|Persistence.Migrations.AppDbContextModelSnapshot|0%||
|Persistence.Migrations.InitialCreateDb|97.3%||
|Persistence.PersistenceDependency|100%||

</details>
<details><summary>Utils.Tests - 95.2%</summary>

|**Name**|**Line**|**Branch**|
|:---|---:|---:|
|**Utils.Tests**|**95.2%**|**50%**|
|Utils.Tests.Builders.Api.ClientEndpoint.Requests.CreateClientEndpointReques<br/>tBuilder|100%||
|Utils.Tests.Builders.Api.ClientEndpoint.Responses.ClientEndpointResponseBui<br/>lder|100%||
|Utils.Tests.Builders.Api.ProductCategoryEndpoint.Requests.CreateProductCate<br/>goryEndpointRequestBuilder|100%||
|Utils.Tests.Builders.Api.ProductCategoryEndpoint.Responses.ProductCategoryE<br/>ndpointResponseBuilder|100%||
|Utils.Tests.Builders.Api.ProductEndpoint.Requests.CreateProductEndpointRequ<br/>estBuilder|100%||
|Utils.Tests.Builders.Api.ProductEndpoint.Responses.ProductEndpointResponseB<br/>uilder|100%||
|Utils.Tests.Builders.Application.Clients.Requests.CreateClientRequestBuilde<br/>r|100%||
|Utils.Tests.Builders.Application.Clients.Responses.ClientResponseBuilder|100%||
|Utils.Tests.Builders.Application.Orders.Requests.AddOrderItemRequestBuilder|100%||
|Utils.Tests.Builders.Application.Orders.Requests.CheckoutOrderRequestBuilde<br/>r|69.2%||
|Utils.Tests.Builders.Application.Orders.Requests.CreateOrderRequestBuilder|100%||
|Utils.Tests.Builders.Application.Orders.Requests.GetOrderByIdRequestBuilder|100%||
|Utils.Tests.Builders.Application.Orders.Requests.GetOrdersByStatusRequestBu<br/>ilder|84.6%||
|Utils.Tests.Builders.Application.Orders.Requests.UpdateOrderStatusRequestBu<br/>ilder|100%||
|Utils.Tests.Builders.Application.Orders.Responses.CheckoutOrderResponseBuil<br/>der|81.8%||
|Utils.Tests.Builders.Application.Orders.Responses.OrderItemResponseBuilder|72.8%|50%|
|Utils.Tests.Builders.Application.Orders.Responses.OrderTrackingResponseBuil<br/>der|85.7%||
|Utils.Tests.Builders.Application.ProductCategories.Requests.CreateProductCa<br/>tegoryRequestBuilder|100%||
|Utils.Tests.Builders.Application.Products.Requests.CreateProductRequestBuil<br/>der|100%||
|Utils.Tests.Builders.Application.Products.Requests.UpdateProductRequestBuil<br/>der|100%||
|Utils.Tests.Builders.Application.Products.Requests.UploadProductImageReques<br/>tBuilder|100%||
|Utils.Tests.Builders.Domain.Entities.ClientBuilder|100%||
|Utils.Tests.Builders.Domain.Entities.OrderBuilder|90.9%||
|Utils.Tests.Builders.Domain.Entities.OrderItemBuilder|100%||
|Utils.Tests.Builders.Domain.Entities.PaymentBuilder|67.7%||
|Utils.Tests.Builders.Domain.Entities.ProductBuilder|100%||
|Utils.Tests.Builders.Domain.Entities.ProductCategoryBuilder|100%||
|Utils.Tests.Builders.Domain.ValueObjects.MoneyBuilder|100%||

</details>
<details><summary>Web.Api - 44.3%</summary>

|**Name**|**Line**|**Branch**|
|:---|---:|---:|
|**Web.Api**|**44.3%**|**38.7%**|
|Program|100%|100%|
|Web.Api.Endpoints.ApiEndpoints|100%||
|Web.Api.Endpoints.ApiEndpointsExtensions|71.4%|50%|
|Web.Api.Endpoints.Clients.ClientEndpoints|65.4%|70%|
|Web.Api.Endpoints.Clients.Requests.CreateClientEndpointRequest|100%||
|Web.Api.Endpoints.Clients.Requests.Mapping.ClientEndpointRequestMapper|100%||
|Web.Api.Endpoints.Clients.Responses.ClientEndpointResponse|100%||
|Web.Api.Endpoints.Clients.Responses.Mapping.ClientEndpointResponseMapper|63.6%|0%|
|Web.Api.Endpoints.Common.Constants.ContentTypes|0%||
|Web.Api.Endpoints.Orders.OrderEndpoints|33%|42.8%|
|Web.Api.Endpoints.Orders.Requests.AddOrderItemEndpointRequest|0%||
|Web.Api.Endpoints.Orders.Requests.CreateOrderEndpointRequest|0%||
|Web.Api.Endpoints.Orders.Requests.Mapping.OrderEndpointRequestMapper|0%||
|Web.Api.Endpoints.Orders.Requests.UpdateOrderStatusEndpointRequest|0%||
|Web.Api.Endpoints.Orders.Responses.CreateOrderEndpointResponse|0%||
|Web.Api.Endpoints.Orders.Responses.Mapping.OrderEndpointResponseMapper|0%|0%|
|Web.Api.Endpoints.Orders.Responses.OrderCheckoutEndpointResponse|0%||
|Web.Api.Endpoints.Orders.Responses.OrderEndpointResponse|0%||
|Web.Api.Endpoints.Orders.Responses.OrderItemEndpointResponse|0%||
|Web.Api.Endpoints.Orders.Responses.OrderTrackingEndpointDataResponse|0%||
|Web.Api.Endpoints.Orders.Responses.OrderTrackingEndpointResponse|0%||
|Web.Api.Endpoints.ProductCategories.ProductCategoriesEndpoints|50%|62.5%|
|Web.Api.Endpoints.ProductCategories.Requests.CreateProductCategoryEndpointR<br/>equest|100%||
|Web.Api.Endpoints.ProductCategories.Requests.UpdateProductCategoryEndpointR<br/>equest|0%||
|Web.Api.Endpoints.ProductCategories.Responses.Mapping.ProductCategoryEndpoi<br/>ntResponseMapper|52.9%|0%|
|Web.Api.Endpoints.ProductCategories.Responses.ProductCategoryEndpointRespon<br/>se|100%||
|Web.Api.Endpoints.Products.ProductEndpoints|40%|60.7%|
|Web.Api.Endpoints.Products.Requests.CreateProductEndpointRequest|100%||
|Web.Api.Endpoints.Products.Requests.Mapping.ProductEndpointRequestMapper|100%||
|Web.Api.Endpoints.Products.Requests.UpdateProductEndpointRequest|0%||
|Web.Api.Endpoints.Products.Responses.Mapping.ProductEndpointResponseMapper|40.6%|0%|
|Web.Api.Endpoints.Products.Responses.ProductEndpointResponse|100%||
|Web.Api.Extensions.ApiError|0%||
|Web.Api.Extensions.FluentResultExtensions|0%|0%|
|Web.Api.Swagger.ConfigureSwaggerOptions|88.8%|75%|
|Web.Api.Swagger.SwaggerDefaultValues|0%|0%|

</details>
