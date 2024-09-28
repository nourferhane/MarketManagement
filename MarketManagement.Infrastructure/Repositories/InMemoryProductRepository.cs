using MaretManagement.Domain.Aggregates.Product;
using MaretManagement.Domain.Repositories;

namespace MarketManagement.Infrastructure.Repositories;

public class InMemoryProductRepository : IRepository<Product>
{
    private Product[] _products;

    public InMemoryProductRepository()
    {
        _products = [
        new Product(1, "Produit 1", 5),
        new Product(2, "Produit 2", 1),
        new Product(3, "Produit 3", 2),
        new Product(4, "Produit 4", 9),
        new Product(5, "Produit 5", 10),
        new Product(6, "Produit 6", 11)
              ];
    }

    public void DeleteAll()
    {
        throw new NotImplementedException();
    }

    public Product Get(int id)
    {
        var result = _products.FirstOrDefault(p => p.Id == id);
        return result ?? throw new ArgumentException("Produit non trouvé /géré");
    }

    public Product[] GetAll() => _products;

    public Product Update(Product entity)
    {
        throw new NotImplementedException("non impl poru l'instant");
    }
}
