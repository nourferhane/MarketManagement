using MaretManagement.Domain.Aggregates.ShoppingCart.ValueObjects;

namespace MaretManagement.Domain.Aggregates.ShoppingCart.Entities;

public class CartItem(Amount productPrice, Quantity quantity, int productId)
{
    public int Id { get; private set; } = 0; // géré par persistance
    public int ProductId { get; private set; } = productId;
    public Amount ProductPrice { get; private set; } = productPrice;
    public Quantity Quantity { get; private set; } = quantity;

    public Amount GetTotalPrice() => Amount.AmountFor(ProductPrice.GetValue() * Quantity.GetValue());

    internal void UpdateQuantity(Quantity quantity)
    {
        Quantity = quantity;
    }

}
