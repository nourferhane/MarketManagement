using MaretManagement.Domain.Abstraction;
using MaretManagement.Domain.Aggregates.ShoppingCart.ValueObjects;

namespace MaretManagement.Domain.Aggregates.Product;

public class Product : IAggregate
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Amount Price { get; private set; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = Amount.AmountFor(price);
    }

    public override string ToString()
    {
        return $"{Id} - {Name} - {Price.GetValue()} Eur";
    }
}
