using MaretManagement.Domain.Aggregates.ShoppingCart;
using MaretManagement.Domain.Repositories;

namespace MarketManagement.Infrastructure.Repositories;

public class InMemoryShoppingCartRepository : IRepository<ShoppingCart>
{
    private readonly IList<ShoppingCart> _shoppingCarts = [];

    public void DeleteAll()
    {
        _shoppingCarts.Clear();
    }

    public ShoppingCart Get(int id)
    {
        var result = _shoppingCarts.FirstOrDefault(p => p.Id == id);
        return result ?? new ShoppingCart();
    }

    public ShoppingCart[] GetAll()
    {
        throw new NotImplementedException("non impl poru l'instant");
    }

    public ShoppingCart Update(ShoppingCart entity)
    {
        if(_shoppingCarts.FirstOrDefault() == null)
        {
            _shoppingCarts.Add(entity);
        };
        return _shoppingCarts[0];
    }
}
