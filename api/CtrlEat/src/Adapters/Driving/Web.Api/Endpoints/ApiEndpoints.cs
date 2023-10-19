namespace Web.Api.Endpoints;

public static class ApiEndpoints
{
    private const string ApiBase = "/api/v{version:apiVersion}";

    public static class Clients
    {
        public static readonly string BaseRoute = $"{ApiBase}/clients";

        public static class V1
        {
            public static readonly double Version = 1.0;

            public static readonly string Create = $"CreateClient:{Version}";
            public static readonly string GetAll = $"GetAllClients:{Version}";
            public static readonly string GetById = $"GetClientById:{Version}";
        }
    }

    public static class Orders
    {
        public static readonly string BaseRoute = $"{ApiBase}/orders";

        public static class V1
        {
            public static readonly double Version = 1.0;

            public static readonly string GetById = $"GetOrderById:{Version}";
            public static readonly string GetByStatus = $"GetORderByStatus:{Version}";
            public static readonly string Create = $"CreateOrder:{Version}";
            public static readonly string UpdateStatus = $"UpdateOrderStatus:{Version}";
            public static readonly string AddOrderItem = $"AddOrderItem:{Version}";
            public static readonly string Checkout = $"CheckoutOrder:{Version}";
        }
    }

    public static class Products
    {
        public static readonly string BaseRoute = $"{ApiBase}/products";

        public static class V1
        {
            public static readonly double Version = 1.0;

            public static readonly string GetById = $"GetProductById:{Version}";
            public static readonly string GetAll = $"GetAllProducts:{Version}";
            public static readonly string GetAllByCategory = $"GetAllProductsByCategory:{Version}";
            public static readonly string Create = $"CreateProduct:{Version}";
            public static readonly string GetImage = $"GetProductImage:{Version}";
            public static readonly string UploadImage = $"UploadProductImage:{Version}";
            public static readonly string Update = $"UpdateProduct:{Version}";
            public static readonly string Delete = $"DeleteProduct:{Version}";
        }
    }

    public static class ProductCategories
    {
        public static readonly string BaseRoute = $"{ApiBase}/products/categories";

        public static class V1
        {
            public static readonly double Version = 1.0;

            public static readonly string GetById = $"GetProductCategoryById:{Version}";
            public static readonly string GetAll = $"GetAllProductCategories:{Version}";
            public static readonly string Create = $"CreateProductCategory:{Version}";
            public static readonly string Update = $"UpdateProductCategory:{Version}";
            public static readonly string Delete = $"DeleteProductCategory:{Version}";
        }
    }
}
