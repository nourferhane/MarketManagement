using MaretManagement.Domain.Aggregates.Product;
using MaretManagement.Domain.Aggregates.ShoppingCart;
using MaretManagement.Domain.Repositories;
using MarketManagement;
using MarketManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = ConfigureServices();

        var app = serviceProvider.GetRequiredService<Application>();

        app.Run();
    }

    private static ServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<IRepository<Product>, InMemoryProductRepository>();
        serviceCollection.AddSingleton<IRepository<ShoppingCart>, InMemoryShoppingCartRepository>();
        serviceCollection.AddSingleton<Application>();

        return serviceCollection.BuildServiceProvider();
    }
}